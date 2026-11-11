using GameStore.Application.Dtos.General;
using GameStore.Application.Features.ConsoleVideoGames.Requests.Commands;
using GameStore.Application.Features.ConsoleVideoGames.Requests.Queries;
using GameStore.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class ConsoleVideoGameController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route("CreateAsync")]
        [ProducesResponseType(typeof(HttpResponseDto<ConsoleVideoGameDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateConsoleVideoGameRequest createConsoleVideoGameRequest)
        {
            var httpResponseDto = await mediator.Send(createConsoleVideoGameRequest);
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpDelete]
        [Route("DeleteAsync/{id}")]
        [ProducesResponseType(typeof(HttpResponseDto<ConsoleVideoGameDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var httpResponseDto = await mediator.Send(new DeleteConsoleVideoGameRequest { Id = id });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadAllAsync")]
        [ProducesResponseType(typeof(HttpResponseDto<ConsoleVideoGameDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReadAllAsync()
        {
            var httpResponseDto = await mediator.Send(new ReadConsoleVideoGameAllRequest());
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadByIdAsync/{id}")]
        [ProducesResponseType(typeof(HttpResponseDto<ConsoleVideoGameDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReadByIdAsync(int id)
        {
            var httpResponseDto = await mediator.Send(new ReadConsoleVideoGameByIdRequest { Id = id });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpPut]
        [Route("UpdateAsync")]
        [ProducesResponseType(typeof(HttpResponseDto<ConsoleVideoGameDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateConsoleVideoGameRequest updateConsoleVideoGameRequest)
        {
            var httpResponseDto = await mediator.Send(updateConsoleVideoGameRequest);
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }
    }
}
