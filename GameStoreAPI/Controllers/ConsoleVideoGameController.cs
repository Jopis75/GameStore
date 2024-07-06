using Application.Dtos.ConsoleVideoGames;
using Application.Dtos.General;
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
        [ProducesResponseType(typeof(HttpResponseDto<CreateConsoleVideoGameResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<CreateConsoleVideoGameResponseDto>>> CreateAsync([FromBody] CreateConsoleVideoGameRequestDto createConsoleVideoGameRequestDto)
        {
            var httpResponseDto = await _mediator.Send(new CreateConsoleVideoGameRequest
            {
                ConsoleVideoGameDto = createConsoleVideoGameRequestDto
            });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [ProducesResponseType(typeof(HttpResponseDto<DeleteConsoleVideoGameResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<DeleteConsoleVideoGameResponseDto>>> DeleteAsync(int id)
        {
            var httpResponseDto = await _mediator.Send(new DeleteConsoleVideoGameRequest
            {
                Id = new DeleteConsoleVideoGameRequestDto
                {
                    Id = id
                }
            });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadAll")]
        [ProducesResponseType(typeof(HttpResponseDto<ReadConsoleVideoGameResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<ReadConsoleVideoGameResponseDto>>> ReadAllAsync()
        {
            var httpResponseDto = await _mediator.Send(new ReadConsoleVideoGameAllRequest());
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadById/{id}")]
        [ProducesResponseType(typeof(HttpResponseDto<ReadConsoleVideoGameResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<ReadConsoleVideoGameResponseDto>>> ReadByIdAsync(int id)
        {
            var httpResponseDto = await _mediator.Send(new ReadConsoleVideoGameByIdRequest
            {
                Id = new ReadByIdRequestDto
                {
                    Id = id
                }
            });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(typeof(HttpResponseDto<UpdateConsoleVideoGameResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<UpdateConsoleVideoGameResponseDto>>> UpdateAsync([FromBody] UpdateConsoleVideoGameRequestDto updateConsoleVideoGameRequestDto)
        {
            var httpResponseDto = await _mediator.Send(new UpdateConsoleVideoGameRequest
            {
                ConsoleVideoGameDto = updateConsoleVideoGameRequestDto
            });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }
    }
}
