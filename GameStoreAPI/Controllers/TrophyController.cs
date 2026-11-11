using GameStore.Application.Dtos.General;
using GameStore.Application.Features.Trophies.Requests.Commands;
using GameStore.Application.Features.Trophies.Requests.Queries;
using GameStore.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class TrophyController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route("CreateAsync")]
        [ProducesResponseType(typeof(HttpResponseDto<TrophyDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTrophyRequest createTrophyRequest)
        {
            var httpResponseDto = await mediator.Send(createTrophyRequest);
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpDelete]
        [Route("DeleteAsync/{id}")]
        [ProducesResponseType(typeof(HttpResponseDto<TrophyDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var httpResponseDto = await mediator.Send(new DeleteTrophyRequest { Id = id });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadAllAsync")]
        [ProducesResponseType(typeof(HttpResponseDto<TrophyDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReadAllAsync()
        {
            var httpResponseDto = await mediator.Send(new ReadTrophyAllRequest());
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadByIdAsync/{id}")]
        [ProducesResponseType(typeof(HttpResponseDto<TrophyDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReadByIdAsync(int id)
        {
            var httpResponseDto = await mediator.Send(new ReadTrophyByIdRequest { Id = id });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpPut]
        [Route("UpdateAsync")]
        [ProducesResponseType(typeof(HttpResponseDto<TrophyDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateTrophyRequest updateTrophyRequest)
        {
            var httpResponseDto = await mediator.Send(updateTrophyRequest);
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }
    }
}