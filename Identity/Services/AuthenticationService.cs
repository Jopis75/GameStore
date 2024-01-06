using Application.Exceptions;
using Application.Interfaces.Identity;
using Application.Models.Identity;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly JwtSettings _jwtSettings;

        public AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
        }

        public async Task<AuthenticationResponse> Login(AuthenticationRequest authenticationRequest)
        {
            var applicationUser = await _userManager.FindByEmailAsync(authenticationRequest.UserName);

            if (applicationUser == null)
            {
                throw new NotFoundException($"Could not find user {authenticationRequest.UserName}.", authenticationRequest.UserName);
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(applicationUser, authenticationRequest.Password, false);

            if (!signInResult.Succeeded)
            {
                var message = $"The user {authenticationRequest.UserName} failed to sign-in.";

                if (signInResult.IsLockedOut)
                {
                    message = $"The user {authenticationRequest.UserName} is locked out.";
                }
                else if (signInResult.IsNotAllowed)
                {
                    message = $"The user {authenticationRequest.UserName} is not allowed to sign-in.";
                }
                else if (signInResult.RequiresTwoFactor)
                {
                    message = $"The user {authenticationRequest.UserName} requires two factor authentication.";
                }

                throw new BadRequestException(message);
            }

            var jwtSecurityToken = await GenerateJwtSecurityTokenAsync(applicationUser);

            return new AuthenticationResponse
            {
                Id = applicationUser.Id,
                JwtSecurityToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserName = applicationUser.UserName,
                Email = applicationUser.Email  
            };
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest registrationRequest)
        {
            var applicationUser = new ApplicationUser
            {
                UserName = registrationRequest.UserName,
                FirstName = registrationRequest.FirstName,
                LastName = registrationRequest.LastName,
                Email = registrationRequest.Email,
                EmailConfirmed = true
            };

            var identityResult = await _userManager.CreateAsync(applicationUser, registrationRequest.Password);

            if (!identityResult.Succeeded)
            {
                var stringBuilder = new StringBuilder();

                foreach (var error in identityResult.Errors)
                {
                    stringBuilder.AppendLine($"{error.Code}: {error.Description}");
                }

                throw new BadRequestException(stringBuilder.ToString());
            }

            await _userManager.AddToRoleAsync(applicationUser, "User");

            return new RegistrationResponse
            {
                UserId = applicationUser.Id
            };
        }

        private async Task<JwtSecurityToken> GenerateJwtSecurityTokenAsync(ApplicationUser applicationUser)
        {
            var userClaims = await _userManager.GetClaimsAsync(applicationUser);

            var roleClaims = (await _userManager
                .GetRolesAsync(applicationUser))
                .Select(role => new Claim(ClaimTypes.Role, role))
                .ToList();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, applicationUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),
                new Claim("uid", applicationUser.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecuritykey, SecurityAlgorithms.HmacSha256);
            
            return new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials
            );
        }
    }
}
