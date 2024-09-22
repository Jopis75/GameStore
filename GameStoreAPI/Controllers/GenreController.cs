using Application.Dtos.General;
using Application.Features.Genres.Requests.Commands;
using Application.Features.Genres.Requests.Queries;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GenreController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [Route("CreateAsync")]
        [ProducesResponseType(typeof(HttpResponseDto<GenreDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<GenreDto>>> CreateAsync([FromBody] CreateGenreRequest createGenreRequest)
        {
            var httpResponseDto = await _mediator.Send(createGenreRequest);
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpDelete]
        [Route("DeleteAsync/{id}")]
        [ProducesResponseType(typeof(HttpResponseDto<GenreDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<GenreDto>>> DeleteAsync(int id)
        {
            var httpResponseDto = await _mediator.Send(new DeleteGenreRequest { Id = id });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadAllAsync")]
        [ProducesResponseType(typeof(HttpResponseDto<GenreDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<GenreDto>>> ReadAllAsync()
        {
            var httpResponseDto = await _mediator.Send(new ReadGenreAllRequest());
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadByIdAsync/{id}")]
        [ProducesResponseType(typeof(HttpResponseDto<GenreDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<GenreDto>>> ReadByIdAsync(int id)
        {
            var httpResponseDto = await _mediator.Send(new ReadGenreByIdRequest { Id = id });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpPut]
        [Route("UpdateAsync")]
        [ProducesResponseType(typeof(HttpResponseDto<GenreDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<GenreDto>>> UpdateAsync([FromBody] UpdateGenreRequest updateGenreRequest)
        {
            var httpResponseDto = await _mediator.Send(updateGenreRequest);
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }
    }
}