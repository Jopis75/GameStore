using Application.Dtos.General;
using Application.Features.VideoGames.Requests.Commands;
using Application.Interfaces.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.VideoGames.RequestHandlers.Commands
{
    public class UploadGameStoreFileRequestHandler : IRequestHandler<UploadGameStoreFileRequest, HttpResponseDto<UploadGameStoreFileDto>>
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

        public async Task<HttpResponseDto<UploadGameStoreFileDto>> Handle(UploadGameStoreFileRequest uploadGameStoreRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin UploadGameStoreFile {@UploadGameStoreFileRequest}.", uploadGameStoreRequest);

                if (uploadGameStoreRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<UploadGameStoreFileDto>(new ArgumentNullException(nameof(uploadGameStoreRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UploadGameStoreFile {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(uploadGameStoreRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<UploadGameStoreFileDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UploadGameStoreFile {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                await _gameStoreFileService.UpsertAsync(uploadGameStoreRequest.FormFile, cancellationToken);

                var uploadGameStoreDto = new UploadGameStoreFileDto();

                var httpResponseDto = new HttpResponseDto<UploadGameStoreFileDto>(uploadGameStoreDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done UploadGameStoreFile {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<UploadGameStoreFileDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled UploadGameStoreFile {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<UploadGameStoreFileDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error UploadGameStoreFile {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
