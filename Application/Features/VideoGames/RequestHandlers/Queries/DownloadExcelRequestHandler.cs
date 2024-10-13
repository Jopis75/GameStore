using Application.Dtos.General;
using Application.Dtos.General.Interfaces;
using Application.Features.VideoGames.Requests.Queries;
using Application.Interfaces.Persistance;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.VideoGames.RequestHandlers.Queries
{
    public class DownloadExcelRequestHandler : IRequestHandler<DownloadExcelRequest, HttpResponseDto<DownloadExcelDto>>
    {
        private readonly IVideoGameRepository _videoGameRepository;

        private readonly IValidator<DownloadExcelRequest> _validator;

        private readonly ILogger<DownloadExcelRequestHandler> _logger;

        public DownloadExcelRequestHandler(IVideoGameRepository videoGameRepository, IValidator<DownloadExcelRequest> validator, ILogger<DownloadExcelRequestHandler> logger)
        {
            _videoGameRepository = videoGameRepository ?? throw new ArgumentNullException(nameof(videoGameRepository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<DownloadExcelDto>> Handle(DownloadExcelRequest downloadExcelRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin DownloadExcel {@DownloadExcelRequest}.", downloadExcelRequest);

                if (downloadExcelRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<DownloadExcelDto>(new ArgumentNullException(nameof(downloadExcelRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error DownloadExcel {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(downloadExcelRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<DownloadExcelDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error DownloadExcel {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var videoGameDtos = await _videoGameRepository.ReadByConsoleIdAsync(downloadExcelRequest.ConsoleId, cancellationToken);

                // Use ClosedXML package here.
                // https://learn.microsoft.com/en-us/answers/questions/835030/how-to-download-as-excel-file-from-selected-rows-i

                var downloadExcelDto = new DownloadExcelDto
                {
                    ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    FileContents = new byte[0],
                    FileDownloadName = downloadExcelRequest.FileDownloadName
                };

                var httpResponseDto = new HttpResponseDto<DownloadExcelDto>(downloadExcelDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done DownloadExcel {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;

            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<DownloadExcelDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled DownloadExcel {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<DownloadExcelDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error DownloadExcel {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
