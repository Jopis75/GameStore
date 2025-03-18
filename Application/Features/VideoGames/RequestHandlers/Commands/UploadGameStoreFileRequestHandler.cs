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
    public class UploadGameStoreFileRequestHandler : IRequestHandler<UploadGameStoreFileRequest, HttpResponseDto<UploadGameStoreFileDto<VideoGameDto>>>
    {
        private readonly IGameStoreFileService _gameStoreFileService;

        private readonly IValidator<UploadGameStoreFileRequest> _validator;

        private readonly ILogger<UploadGameStoreFileRequestHandler> _logger;

        public UploadGameStoreFileRequestHandler(IGameStoreFileService gameStoreFileService, IValidator<UploadGameStoreFileRequest> validator, ILogger<UploadGameStoreFileRequestHandler> logger)
        {
            _gameStoreFileService = gameStoreFileService;
            _validator = validator;
            _logger = logger;
        }

        public async Task<HttpResponseDto<UploadGameStoreFileDto<VideoGameDto>>> Handle(UploadGameStoreFileRequest uploadGameStoreFileRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin UploadGameStoreFile {@UploadGameStoreFileRequest}.", uploadGameStoreFileRequest);

                if (uploadGameStoreFileRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<UploadGameStoreFileDto<VideoGameDto>>(new ArgumentNullException(nameof(uploadGameStoreFileRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UploadGameStoreFile {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(uploadGameStoreFileRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<UploadGameStoreFileDto<VideoGameDto>>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UploadGameStoreFile {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var uploadGameStoreFileDto = await _gameStoreFileService.UpsertAsync(uploadGameStoreFileRequest.FormFile, cancellationToken);

                var httpResponseDto = new HttpResponseDto<UploadGameStoreFileDto<VideoGameDto>>(uploadGameStoreFileDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done UploadGameStoreFile {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<UploadGameStoreFileDto<VideoGameDto>>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled UploadGameStoreFile {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<UploadGameStoreFileDto<VideoGameDto>>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error UploadGameStoreFile {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
