using Application.Dtos.General;
using Application.Features.VideoGames.Requests.Queries;
using Application.Interfaces.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.VideoGames.RequestHandlers.Queries
{
    public class DownloadExcelFileByConsoleIdRequestHandler : IRequestHandler<DownloadExcelFileByConsoleIdRequest, HttpResponseDto<FileDownloadDto>>
    {
        private readonly IExcelFileService _excelFileService;

        private readonly IValidator<DownloadExcelFileByConsoleIdRequest> _validator;

        private readonly ILogger<DownloadExcelFileByConsoleIdRequestHandler> _logger;

        public DownloadExcelFileByConsoleIdRequestHandler(IExcelFileService excelFileService, IValidator<DownloadExcelFileByConsoleIdRequest> validator, ILogger<DownloadExcelFileByConsoleIdRequestHandler> logger)
        {
            _excelFileService = excelFileService;
            _validator = validator;
            _logger = logger;
        }

        public async Task<HttpResponseDto<FileDownloadDto>> Handle(DownloadExcelFileByConsoleIdRequest downloadExcelFileByConsoleIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin DownloadExcelFileByConsoleId {@DownloadExcelFileByConsoleIdRequest}.", downloadExcelFileByConsoleIdRequest);

                if (downloadExcelFileByConsoleIdRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(downloadExcelFileByConsoleIdRequest));
                    var httpResponseDto1 = new HttpResponseDto<FileDownloadDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error DownloadExcelFileByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(downloadExcelFileByConsoleIdRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<FileDownloadDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error DownloadExcelFileByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var fileDownloadDto = await _excelFileService.CreateFileDownloadForVideoGamesByConsoleIdAsync(downloadExcelFileByConsoleIdRequest.ConsoleId, downloadExcelFileByConsoleIdRequest.FileDownloadName, cancellationToken);

                var httpResponseDto = new HttpResponseDto<FileDownloadDto>(fileDownloadDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done DownloadExcelFileByConsoleId {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<FileDownloadDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Canceled DownloadExcelFileByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<FileDownloadDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error DownloadExcelFileByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
