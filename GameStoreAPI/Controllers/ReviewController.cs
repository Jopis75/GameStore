using Application.Dtos.Reviews;
using Application.Features.Reviews.Requests.Commands;
using Application.Features.Reviews.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReviewController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(typeof(CreateReviewResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CreateReviewResponseDto>> CreateAsync([FromBody] CreateReviewRequestDto createReviewRequestDto)
        {
            var httpResponseDto = await _mediator.Send(new CreateReviewRequest
            {
                CreateReviewRequestDto = createReviewRequestDto
            });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [ProducesResponseType(typeof(DeleteReviewResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DeleteReviewResponseDto>> DeleteAsync(int id)
        {
            var httpResponseDto = await _mediator.Send(new DeleteReviewRequest
            {
                DeleteReviewRequestDto = new DeleteReviewRequestDto
                {
                    Id = id
                }
            });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadAll")]
        [ProducesResponseType(typeof(ReadReviewResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReadReviewResponseDto>> ReadAllAsync()
        {
            var httpResponseDto = await _mediator.Send(new ReadAllReviewRequest());
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadById/{id}")]
        [ProducesResponseType(typeof(ReadReviewResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReadReviewResponseDto>> ReadByIdAsync(int id)
        {
            var httpResponseDto = await _mediator.Send(new ReadByIdReviewRequest
            {
                ReadByIdReviewRequestDto = new ReadByIdReviewRequestDto
                {
                    Id = id
                }
            });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        //[HttpGet]
        //[Route("ReadByVideoGameId/{videoGameId}")]
        //[ProducesResponseType(typeof(ReadReviewResponseDto), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult<ReadReviewResponseDto>> ReadByVideoGameIdAsync(int videoGameId)
        //{

        //}

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(typeof(UpdateReviewResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UpdateReviewResponseDto>> UpdateAsync([FromBody] UpdateReviewRequestDto updateReviewRequestDto)
        {
            var httpResponseDto = await _mediator.Send(new UpdateReviewRequest
            {
                UpdateReviewRequestDto = updateReviewRequestDto
            });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }
    }
}
