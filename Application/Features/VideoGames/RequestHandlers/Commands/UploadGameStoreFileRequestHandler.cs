using Application.Dtos.General;
using Application.Features.VideoGames.Requests.Commands;
using Application.Interfaces.Infrastructure;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.VideoGames.RequestHandlers.Commands
{
    public class UploadGameStoreFileRequestHandler(IGameStoreFileService gameStoreFileService, IValidator<UploadGameStoreFileRequest> validator, ILogger<UploadGameStoreFileRequestHandler> logger) : IRequestHandler<UploadGameStoreFileRequest, HttpResponseDto<GameStoreFileUploadResponseDto<VideoGameDto>>>
    {
        public async Task<HttpResponseDto<GameStoreFileUploadResponseDto<VideoGameDto>>> Handle(UploadGameStoreFileRequest uploadGameStoreFileRequest, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Begin UploadGameStoreFile {@UploadGameStoreFileRequest}.", uploadGameStoreFileRequest);

                if (uploadGameStoreFileRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(uploadGameStoreFileRequest));
                    var httpResponseDto1 = new HttpResponseDto<GameStoreFileUploadResponseDto<VideoGameDto>>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error UploadGameStoreFile {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await validator.ValidateAsync(uploadGameStoreFileRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<GameStoreFileUploadResponseDto<VideoGameDto>>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error UploadGameStoreFile {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var gameStoreFileUploadDto = await gameStoreFileService.UploadAsync(uploadGameStoreFileRequest.FormFile, cancellationToken);

                var httpResponseDto = new HttpResponseDto<GameStoreFileUploadResponseDto<VideoGameDto>>(gameStoreFileUploadDto, StatusCodes.Status200OK);
                logger.LogInformation("Done UploadGameStoreFile {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<GameStoreFileUploadResponseDto<VideoGameDto>>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled UploadGameStoreFile {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<GameStoreFileUploadResponseDto<VideoGameDto>>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error UploadGameStoreFile {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
