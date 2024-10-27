using Application.Dtos.General;
using Application.Features.VideoGames.Requests.Commands;
using Application.Features.VideoGames.Requests.Queries;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace GameStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VideoGameController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [Route("CreateAsync")]
        [ProducesResponseType(typeof(HttpResponseDto<VideoGameDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<VideoGameDto>>> CreateAsync([FromBody] CreateVideoGameRequest createVideoGameRequest)
        {
            var httpResponseDto = await _mediator.Send(createVideoGameRequest);
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpDelete]
        [Route("DeleteAsync/{id}")]
        [ProducesResponseType(typeof(HttpResponseDto<VideoGameDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<VideoGameDto>>> DeleteAsync(int id)
        {
            var httpResponseDto = await _mediator.Send(new DeleteVideoGameRequest { Id = id });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpPost]
        [Route("DownloadExcelAsync")]
        [ProducesResponseType(typeof(HttpResponseDto<DownloadExcelDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<DownloadExcelDto>>> DownloadExcelAsync([FromBody] DownloadExcelRequest downloadExcelRequest)
        {
            var httpResponseDto = await _mediator.Send(downloadExcelRequest);
            return httpResponseDto.Successful
                ? File(httpResponseDto.Data[0].FileContents, httpResponseDto.Data[0].ContentType, httpResponseDto.Data[0].FileDownloadName)
                : StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadAllAsync")]
        [ProducesResponseType(typeof(HttpResponseDto<VideoGameDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<VideoGameDto>>> ReadAllAsync()
        {
            var httpResponseDto = await _mediator.Send(new ReadVideoGameAllRequest());
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadByIdAsync/{id}")]
        [ProducesResponseType(typeof(HttpResponseDto<VideoGameDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<VideoGameDto>>> ReadByIdAsync(int id)
        {
            var httpResponseDto = await _mediator.Send(new ReadVideoGameByIdRequest { Id = id });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpGet]
        [Route("ReadMosrPlayedByConsoleIdAsync/{consoleId}")]
        [ProducesResponseType(typeof(HttpResponseDto<VideoGameDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<VideoGameDto>>> ReadMosePlayedByConsoleIdAsync(int consoleId)
        {
            var httpResponseDto = await _mediator.Send(new ReadMostPlayedVideoGameByConsoleIdRequest { ConsoleId = consoleId });
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpPut]
        [Route("UpdateAsync")]
        [ProducesResponseType(typeof(HttpResponseDto<VideoGameDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<VideoGameDto>>> UpdateAsync([FromBody] UpdateVideoGameRequest updateVideoGameRequest)
        {
            var httpResponseDto = await _mediator.Send(updateVideoGameRequest);
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }

        [HttpPost]
        [Route("UploadGameStoreAsync")]
        [ProducesResponseType(typeof(HttpResponseDto<UploadGameStoreDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponseDto<UploadGameStoreDto>>> UploadGameStoreAsync([FromBody] UploadGameStoreRequest uploadGameStoreRequest)
        {
            var httpResponseDto = await _mediator.Send(uploadGameStoreRequest);
            return StatusCode(httpResponseDto.StatusCode, httpResponseDto);
        }
    }
}
