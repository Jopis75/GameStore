using Application.Dtos.General;
using Application.Dtos.Identity;
using Application.Exceptions;
using Application.Interfaces.Identity;
using Application.Models.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
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

        private readonly IValidator<LoginRequestDto> _loginRequestDtoValidator;

        private readonly IValidator<RegistrationRequestDto> _registrationRequestDtoValidator;

        private readonly ILogger<AuthenticationService> _logger;

        private readonly IOptions<JwtSettings> _options;

        public AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IValidator<LoginRequestDto> loginRequestDtoValidator, IValidator<RegistrationRequestDto> registrationRequestDtoValidator, ILogger<AuthenticationService> logger, IOptions<JwtSettings> options)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _loginRequestDtoValidator = loginRequestDtoValidator ?? throw new ArgumentNullException(nameof(loginRequestDtoValidator));
            _registrationRequestDtoValidator = registrationRequestDtoValidator ?? throw new ArgumentNullException(nameof(registrationRequestDtoValidator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<HttpResponseDto<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequestDto)
        {
            try
            {
                _logger.LogInformation("Begin LoginAsync {@LoginRequestDto}.", loginRequestDto);

                if (loginRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<LoginResponseDto>(new ArgumentNullException(nameof(loginRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error LoginAsync {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _loginRequestDtoValidator.ValidateAsync(loginRequestDto);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<LoginResponseDto>(new FluentValidation.ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error LoginAsync {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var applicationUser = await _userManager.FindByEmailAsync(loginRequestDto.UserName);

                if (applicationUser == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<LoginResponseDto>(new NotFoundException($"Could not find user {loginRequestDto.UserName}.", loginRequestDto.UserName).Message, StatusCodes.Status404NotFound);
                    _logger.LogError("Error LoginAsync {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var signInResult = await _signInManager.CheckPasswordSignInAsync(applicationUser, loginRequestDto.Password, false);

                if (!signInResult.Succeeded)
                {
                    var message = $"The user {loginRequestDto.UserName} failed to sign-in.";

                    if (signInResult.IsLockedOut)
                    {
                        message = $"The user {loginRequestDto.UserName} is locked out.";
                    }
                    else if (signInResult.IsNotAllowed)
                    {
                        message = $"The user {loginRequestDto.UserName} is not allowed to sign-in.";
                    }
                    else if (signInResult.RequiresTwoFactor)
                    {
                        message = $"The user {loginRequestDto.UserName} requires two factor authentication.";
                    }

                    var httpResponseDto1 = new HttpResponseDto<LoginResponseDto>(new BadRequestException(message).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error LoginAsync {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var jwtSecurityToken = await GenerateJwtSecurityTokenAsync(applicationUser);

                var loginResponseDto = new LoginResponseDto
                {
                    UserId = applicationUser.Id,
                    JwtSecurityToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    UserName = applicationUser.UserName,
                    Email = applicationUser.Email
                };

                var httpResponseDto = new HttpResponseDto<LoginResponseDto>(loginResponseDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done LoginAsync {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<LoginResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error LoginAsync {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }

        public async Task<HttpResponseDto<RegistrationResponseDto>> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            try
            {
                _logger.LogInformation("Begin RegisterAsync {@RegistrationRequestDto}.", registrationRequestDto);

                if (registrationRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<RegistrationResponseDto>(new ArgumentNullException(nameof(registrationRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error RegisterAsync {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _registrationRequestDtoValidator.ValidateAsync(registrationRequestDto);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<RegistrationResponseDto>(new FluentValidation.ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error RegisterAsync {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var applicationUser = new ApplicationUser
                {
                    UserName = registrationRequestDto.UserName,
                    FirstName = registrationRequestDto.FirstName,
                    LastName = registrationRequestDto.LastName,
                    Email = registrationRequestDto.Email,
                    EmailConfirmed = true
                };

                var identityResult = await _userManager.CreateAsync(applicationUser, registrationRequestDto.Password);

                if (!identityResult.Succeeded)
                {
                    var stringBuilder = new StringBuilder();

                    foreach (var error in identityResult.Errors)
                    {
                        stringBuilder.AppendLine($"{error.Code}: {error.Description}");
                    }

                    var httpResponseDto1 = new HttpResponseDto<RegistrationResponseDto>(new BadRequestException(stringBuilder.ToString()).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error RegisterAsync {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                await _userManager.AddToRoleAsync(applicationUser, registrationRequestDto.Role.ToString());

                var registrationResponseDto = new RegistrationResponseDto
                {
                    UserId = applicationUser.Id
                };

                var httpResponseDto = new HttpResponseDto<RegistrationResponseDto>(registrationResponseDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done RegisterAsync {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<RegistrationResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error RegisterAsync {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
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

            var symmetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key));
            var signingCredentials = new SigningCredentials(symmetricSecuritykey, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer: _options.Value.Issuer,
                audience: _options.Value.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_options.Value.DurationInMinutes),
                signingCredentials: signingCredentials
            );
        }
    }
}
