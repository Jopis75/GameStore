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
using ValidationException = FluentValidation.ValidationException;

namespace Identity.Services
{
    public class AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IValidator<LoginRequestDto> loginRequestDtoValidator, IValidator<RegistrationRequestDto> registrationRequestDtoValidator, ILogger<AuthenticationService> logger, IOptions<JwtSettings> options) : IAuthenticationService
    {
        public async Task<HttpResponseDto<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequestDto, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Begin LoginAsync {@LoginRequestDto}.", loginRequestDto);

                if (loginRequestDto == null)
                {
                    var ex = new ArgumentNullException(nameof(loginRequestDto));
                    var httpResponseDto1 = new HttpResponseDto<LoginResponseDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error LoginAsync {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await loginRequestDtoValidator.ValidateAsync(loginRequestDto, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<LoginResponseDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error LoginAsync {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var applicationUser = await userManager.FindByEmailAsync(loginRequestDto.UserName);

                if (applicationUser == null)
                {
                    var ex = new NotFoundException($"Could not find user {loginRequestDto.UserName}.", loginRequestDto.UserName);
                    var httpResponseDto1 = new HttpResponseDto<LoginResponseDto>(ex.Message, StatusCodes.Status404NotFound);
                    logger.LogError(ex, "Error LoginAsync {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var signInResult = await signInManager.CheckPasswordSignInAsync(applicationUser, loginRequestDto.Password, loginRequestDto.LockoutOnFailure);

                if (signInResult.Succeeded == false)
                {
                    string message;

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
                    else
                    {
                        message = $"The user {loginRequestDto.UserName} failed to sign-in.";
                    }

                    var ex = new BadRequestException(message);
                    var httpResponseDto1 = new HttpResponseDto<LoginResponseDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error LoginAsync {@HttpResponseDto}.", httpResponseDto1);
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
                logger.LogInformation("Done LoginAsync {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<LoginResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error LoginAsync {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }

        public async Task<HttpResponseDto<RegistrationResponseDto>> RegisterAsync(RegistrationRequestDto registrationRequestDto, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Begin RegisterAsync {@RegistrationRequestDto}.", registrationRequestDto);

                if (registrationRequestDto == null)
                {
                    var ex = new ArgumentNullException(nameof(registrationRequestDto));
                    var httpResponseDto1 = new HttpResponseDto<RegistrationResponseDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error RegisterAsync {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await registrationRequestDtoValidator.ValidateAsync(registrationRequestDto, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<RegistrationResponseDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error RegisterAsync {@HttpResponseDto}.", httpResponseDto1);
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

                var identityResult = await userManager.CreateAsync(applicationUser, registrationRequestDto.Password);

                if (identityResult.Succeeded == false)
                {
                    var stringBuilder = new StringBuilder();

                    foreach (var error in identityResult.Errors)
                    {
                        stringBuilder.AppendLine($"{error.Code}: {error.Description}");
                    }

                    var ex = new BadRequestException(stringBuilder.ToString());
                    var httpResponseDto1 = new HttpResponseDto<RegistrationResponseDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error RegisterAsync {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                await userManager.AddToRoleAsync(applicationUser, registrationRequestDto.Role.ToString());

                var registrationResponseDto = new RegistrationResponseDto
                {
                    UserId = applicationUser.Id
                };

                var httpResponseDto = new HttpResponseDto<RegistrationResponseDto>(registrationResponseDto, StatusCodes.Status200OK);
                logger.LogInformation("Done RegisterAsync {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<RegistrationResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error RegisterAsync {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }

        private async Task<JwtSecurityToken> GenerateJwtSecurityTokenAsync(ApplicationUser applicationUser)
        {
            var userClaims = await userManager.GetClaimsAsync(applicationUser);

            var roleClaims = (await userManager
                .GetRolesAsync(applicationUser))
                .Select(role => new Claim(ClaimTypes.Role, role))
                .ToList();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, applicationUser.UserName ?? String.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email ?? String.Empty),
                new Claim("uid", applicationUser.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.Key));
            var signingCredentials = new SigningCredentials(symmetricSecuritykey, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer: options.Value.Issuer,
                audience: options.Value.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(options.Value.DurationInMinutes),
                signingCredentials: signingCredentials
            );
        }
    }
}
