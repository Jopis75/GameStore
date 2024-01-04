using Application.Dtos.Common;
using Application.Dtos.ConsoleVideoGames;
using Application.Features.ConsoleVideoGames.Requests.Commands;
using Application.Features.ConsoleVideoGames.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsoleVideoGameController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConsoleVideoGameController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(typeof(CreateConsoleVideoGameResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CreateConsoleVideoGameResponseDto>> CreateAsync([FromBody] CreateConsoleVideoGameRequestDto createConsoleVideoGameRequestDto)
        {
            var httpResponseDto = await _mediator.Send(new CreateConsoleVideoGameRequest
            {
                CreateConsoleVideoGameRequestDto = createConsoleVideoGameRequestDto
            });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [ProducesResponseType(typeof(DeleteConsoleVideoGameResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DeleteConsoleVideoGameResponseDto>> DeleteAsync(int id)
        {
            var httpResponseDto = await _mediator.Send(new DeleteConsoleVideoGameRequest
            {
                DeleteConsoleVideoGameRequestDto = new DeleteConsoleVideoGameRequestDto
                {
                    Id = id
                }
            });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadAll")]
        [ProducesResponseType(typeof(ReadConsoleVideoGameResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReadConsoleVideoGameResponseDto>> ReadAllAsync()
        {
            var httpResponseDto = await _mediator.Send(new ReadConsoleVideoGameAllRequest());
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadById/{id}")]
        [ProducesResponseType(typeof(ReadConsoleVideoGameResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReadConsoleVideoGameResponseDto>> ReadByIdAsync(int id)
        {
            var httpResponseDto = await _mediator.Send(new ReadConsoleVideoGameByIdRequest
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
        [ProducesResponseType(typeof(UpdateConsoleVideoGameResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UpdateConsoleVideoGameResponseDto>> UpdateAsync([FromBody] UpdateConsoleVideoGameRequestDto updateConsoleVideoGameRequestDto)
        {
            var httpResponseDto = await _mediator.Send(new UpdateConsoleVideoGameRequest
            {
                UpdateConsoleVideoGameRequestDto = updateConsoleVideoGameRequestDto
            });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }
    }
}
