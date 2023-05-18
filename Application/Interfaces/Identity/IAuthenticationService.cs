using Application.Models.Identity;

namespace Application.Interfaces.Identity
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> Login(AuthenticationRequest authenticationRequest);

        Task<RegistrationResponse> Register(RegistrationRequest registrationRequest);
    }
}
