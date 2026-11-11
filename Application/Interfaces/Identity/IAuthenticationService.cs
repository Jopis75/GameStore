using GameStore.Application.Dtos.General;
using GameStore.Application.Dtos.Identity;

namespace GameStore.Application.Interfaces.Identity
{
    public interface IAuthenticationService
    {
        Task<HttpResponseDto<LoginResponseDto>> LoginAsync(LoginRequestDto authenticationRequestDto, CancellationToken cancellationToken);

        Task<HttpResponseDto<RegistrationResponseDto>> RegisterAsync(RegistrationRequestDto registrationRequestDto, CancellationToken cancellationToken);
    }
}
