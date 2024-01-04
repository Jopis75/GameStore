using Application.Dtos.Common;
using Application.Dtos.Consoles;
using Application.Features.Consoles.Requests.Commands;
using Application.Features.Consoles.Requests.Queries;
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
        [Route("Create")]
        [ProducesResponseType(typeof(CreateConsoleResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CreateConsoleResponseDto>> CreateAsync([FromBody] CreateConsoleRequestDto createConsoleRequestDto)
        {
            var httpResponseDto = await _mediator.Send(new CreateConsoleRequest
            {
                CreateConsoleRequestDto = createConsoleRequestDto
            });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [ProducesResponseType(typeof(DeleteConsoleResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DeleteConsoleResponseDto>> DeleteAsync(int id)
        {
            var httpResponseDto = await _mediator.Send(new DeleteConsoleRequest
            {
                DeleteConsoleRequestDto = new DeleteConsoleRequestDto
                {
                    Id = id
                }
            });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadAll")]
        [ProducesResponseType(typeof(ReadConsoleResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReadConsoleResponseDto>> ReadAllAsync()
        {
            var httpResponseDto = await _mediator.Send(new ReadConsoleAllRequest());
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadById/{id}")]
        [ProducesResponseType(typeof(ReadConsoleResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReadConsoleResponseDto>> ReadByIdAsync(int id)
        {
            var httpResponseDto = await _mediator.Send(new ReadConsoleByIdRequest
            {
                ReadByIdRequestDto = new ReadByIdRequestDto
                {
                    Id = id
                }
            });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(typeof(UpdateConsoleResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UpdateConsoleResponseDto>> UpdateAsync([FromBody] UpdateConsoleRequestDto updateConsoleRequestDto)
        {
            var httpResponseDto = await _mediator.Send(new UpdateConsoleRequest
            {
                UpdateConsoleRequestDto = updateConsoleRequestDto
            });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }
    }
}
