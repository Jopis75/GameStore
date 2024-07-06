using Application.Dtos.General;
using Application.Dtos.Identity;
using Application.Interfaces.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<HttpResponseDto<LoginResponseDto>>> Login(LoginRequestDto loginRequestDto)
        {
            var httpResponseDto = await _authenticationService.LoginAsync(loginRequestDto);
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<HttpResponseDto<RegistrationResponseDto>>> Register(RegistrationRequestDto registrationRequestDto)
        {
            var httpResponseDto = await _authenticationService.RegisterAsync(registrationRequestDto);
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }
    }
}
