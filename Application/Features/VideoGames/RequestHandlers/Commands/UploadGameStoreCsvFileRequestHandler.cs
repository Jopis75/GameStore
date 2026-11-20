using FluentValidation;
using GameStore.Application.Dtos.General;
using GameStore.Application.Features.VideoGames.Requests.Commands;
using GameStore.Application.Interfaces.Infrastructure;
using GameStore.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace GameStore.Application.Features.VideoGames.RequestHandlers.Commands
{
    public class UploadGameStoreCsvFileRequestHandler(IGameStoreFileService gameStoreFileService, IValidator<UploadGameStoreCsvFileRequest> validator, ILogger<UploadGameStoreCsvFileRequestHandler> logger) : IRequestHandler<UploadGameStoreCsvFileRequest, HttpResponseDto<GameStoreFileUploadResponseDto<VideoGameDto>>>
    {
        public async Task<HttpResponseDto<GameStoreFileUploadResponseDto<VideoGameDto>>> Handle(UploadGameStoreCsvFileRequest uploadGameStoreFileRequest, CancellationToken cancellationToken)
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

                if (!validationResult.IsValid)
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
