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
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("LoginAsync")]
        public async Task<ActionResult<HttpResponseDto<LoginResponseDto>>> LoginAsync(LoginRequestDto loginRequestDto)
        {
            var httpResponseDto = await _authenticationService.LoginAsync(loginRequestDto);
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpPost]
        [Route("RegisterAsync")]
        public async Task<ActionResult<HttpResponseDto<RegistrationResponseDto>>> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            var httpResponseDto = await _authenticationService.RegisterAsync(registrationRequestDto);
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }
    }
}
