using Application.Dtos.General;
using Application.Dtos.Identity;

namespace Application.Interfaces.Identity
{
    public interface IAuthenticationService
    {
        Task<HttpResponseDto<LoginResponseDto>> LoginAsync(LoginRequestDto authenticationRequestDto, CancellationToken cancellationToken);

        Task<HttpResponseDto<RegistrationResponseDto>> RegisterAsync(RegistrationRequestDto registrationRequestDto, CancellationToken cancellationToken);
    }
}
