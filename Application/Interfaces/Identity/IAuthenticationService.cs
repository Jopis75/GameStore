using Application.Dtos.Common;
using Application.Dtos.Identity;

namespace Application.Interfaces.Identity
{
    public interface IAuthenticationService
    {
        Task<HttpResponseDto<LoginResponseDto>> LoginAsync(LoginRequestDto authenticationRequest);

        Task<HttpResponseDto<RegistrationResponseDto>> RegisterAsync(RegistrationRequestDto registrationRequest);
    }
}
