using Application.Dtos.General;
using Application.Dtos.Identity;
using Application.Interfaces.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
    {
        [HttpPost]
        [Route("LoginAsync")]
        public async Task<ActionResult<HttpResponseDto<LoginResponseDto>>> LoginAsync(LoginRequestDto loginRequestDto)
        {
            var httpResponseDto = await authenticationService.LoginAsync(loginRequestDto, CancellationToken.None);
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpPost]
        [Route("RegisterAsync")]
        public async Task<ActionResult<HttpResponseDto<RegistrationResponseDto>>> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            var httpResponseDto = await authenticationService.RegisterAsync(registrationRequestDto, CancellationToken.None);
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }
    }
}
