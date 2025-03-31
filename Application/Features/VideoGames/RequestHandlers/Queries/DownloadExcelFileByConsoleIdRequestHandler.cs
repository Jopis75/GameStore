using Application.Dtos.General;
using Application.Features.VideoGames.Requests.Queries;
using Application.Interfaces.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.VideoGames.RequestHandlers.Queries
{
    public class DownloadExcelFileByConsoleIdRequestHandler(IExcelFileService excelFileService, IValidator<DownloadExcelFileByConsoleIdRequest> validator, ILogger<DownloadExcelFileByConsoleIdRequestHandler> logger) : IRequestHandler<DownloadExcelFileByConsoleIdRequest, HttpResponseDto<FileDownloadResponseDto>>
    {
        public async Task<HttpResponseDto<FileDownloadResponseDto>> Handle(DownloadExcelFileByConsoleIdRequest downloadExcelFileByConsoleIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Begin DownloadExcelFileByConsoleId {@DownloadExcelFileByConsoleIdRequest}.", downloadExcelFileByConsoleIdRequest);

                if (downloadExcelFileByConsoleIdRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(downloadExcelFileByConsoleIdRequest));
                    var httpResponseDto1 = new HttpResponseDto<FileDownloadResponseDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error DownloadExcelFileByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await validator.ValidateAsync(downloadExcelFileByConsoleIdRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<FileDownloadResponseDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error DownloadExcelFileByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var fileDownloadDto = await excelFileService.CreateFileDownloadForVideoGamesByConsoleIdAsync(downloadExcelFileByConsoleIdRequest.ConsoleId, downloadExcelFileByConsoleIdRequest.FileDownloadName, cancellationToken);

                var httpResponseDto = new HttpResponseDto<FileDownloadResponseDto>(fileDownloadDto, StatusCodes.Status200OK);
                logger.LogInformation("Done DownloadExcelFileByConsoleId {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<FileDownloadResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled DownloadExcelFileByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<FileDownloadResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error DownloadExcelFileByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
