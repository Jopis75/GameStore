using GameStore.Application.Dtos.General;
using GameStore.Application.Features.Reviews.Requests.Commands;
using GameStore.Application.Features.Reviews.Requests.Queries;
using GameStore.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class ReviewController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route("CreateAsync")]
        [ProducesResponseType(typeof(HttpResponseDto<ReviewDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateReviewRequest createReviewRequest)
        {
            var httpResponseDto = await mediator.Send(createReviewRequest);
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpDelete]
        [Route("DeleteAsync/{id}")]
        [ProducesResponseType(typeof(HttpResponseDto<ReviewDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var httpResponseDto = await mediator.Send(new DeleteReviewRequest { Id = id });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadAllAsync")]
        [ProducesResponseType(typeof(HttpResponseDto<ReviewDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReadAllAsync()
        {
            var httpResponseDto = await mediator.Send(new ReadReviewAllRequest());
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadByIdAsync/{id}")]
        [ProducesResponseType(typeof(HttpResponseDto<ReviewDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReadByIdAsync(int id)
        {
            var httpResponseDto = await mediator.Send(new ReadReviewByIdRequest { Id = id });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadByVideoGameIdAsync/{videoGameId}")]
        [ProducesResponseType(typeof(HttpResponseDto<ReviewDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReadByVideoGameIdAsync(int videoGameId)
        {
            var httpResponseDto = await mediator.Send(new ReadReviewsByVideoGameIdRequest { VideoGameId = videoGameId });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpPut]
        [Route("UpdateAsync")]
        [ProducesResponseType(typeof(HttpResponseDto<ReviewDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateReviewRequest updateReviewRequest)
        {
            var httpResponseDto = await mediator.Send(updateReviewRequest);
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }
    }
}
