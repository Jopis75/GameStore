using Application.Dtos.General;
using Application.Features.Consoles.Requests.Commands;
using Application.Features.Consoles.Requests.Queries;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConsoleController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [Route("CreateAsync")]
        [ProducesResponseType(typeof(HttpResponseDto<ConsoleDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<ConsoleDto>>> CreateAsync([FromBody] ConsoleDto consoleDto)
        {
            var httpResponseDto = await _mediator.Send(new CreateConsoleRequest() { ConsoleDto = consoleDto });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpDelete]
        [Route("DeleteAsync/{id}")]
        [ProducesResponseType(typeof(HttpResponseDto<ConsoleDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<ConsoleDto>>> DeleteAsync(int id)
        {
            var httpResponseDto = await _mediator.Send(new DeleteConsoleRequest() { Id = id });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadAllAsync")]
        [ProducesResponseType(typeof(HttpResponseDto<List<ConsoleDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<List<ConsoleDto>>>> ReadAllAsync()
        {
            var httpResponseDto = await _mediator.Send(new ReadConsoleAllRequest());
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadByIdAsync/{id}")]
        [ProducesResponseType(typeof(HttpResponseDto<ConsoleDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<ConsoleDto>>> ReadByIdAsync(int id)
        {
            var httpResponseDto = await _mediator.Send(new ReadConsoleByIdRequest() { Id = id });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpPut]
        [Route("UpdateAsync")]
        [ProducesResponseType(typeof(HttpResponseDto<ConsoleDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<ConsoleDto>>> UpdateAsync([FromBody] ConsoleDto consoleDto)
        {
            var httpResponseDto = await _mediator.Send(new UpdateConsoleRequest() { ConsoleDto = consoleDto });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }
    }
}
