using GameStore.Application.Dtos.Identity;
using GameStore.Application.Interfaces.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
    {
        [HttpPost]
        [Route("LoginAsync")]
        public async Task<IActionResult> LoginAsync(LoginRequestDto loginRequestDto)
        {
            var httpResponseDto = await authenticationService.LoginAsync(loginRequestDto, CancellationToken.None);
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpPost]
        [Route("RegisterAsync")]
        public async Task<IActionResult> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            var httpResponseDto = await authenticationService.RegisterAsync(registrationRequestDto, CancellationToken.None);
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }
    }
}
