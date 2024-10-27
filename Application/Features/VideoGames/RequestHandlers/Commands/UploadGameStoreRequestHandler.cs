using Application.Dtos.General;
using Application.Features.VideoGames.Requests.Commands;
using Application.Interfaces.Persistance;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.VideoGames.RequestHandlers.Commands
{
    public class UploadGameStoreRequestHandler : IRequestHandler<UploadGameStoreRequest, HttpResponseDto<UploadGameStoreDto>>
    {
        private readonly IVideoGameRepository _videoGameRepository;

        private readonly IValidator<UploadGameStoreRequest> _validator;

        private readonly ILogger<UploadGameStoreRequestHandler> _logger;

        public UploadGameStoreRequestHandler(IVideoGameRepository videoGameRepository, IValidator<UploadGameStoreRequest> validator, ILogger<UploadGameStoreRequestHandler> logger)
        {
            _videoGameRepository = videoGameRepository ?? throw new ArgumentNullException(nameof(videoGameRepository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<UploadGameStoreDto>> Handle(UploadGameStoreRequest uploadGameStoreRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin UploadGameStore {@UploadGameStoreRequest}.", uploadGameStoreRequest);

                if (uploadGameStoreRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<UploadGameStoreDto>(new ArgumentNullException(nameof(uploadGameStoreRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UploadGameStore {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(uploadGameStoreRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<UploadGameStoreDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UploadGameStore {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var fileName = uploadGameStoreRequest.FormFile.FileName;
                var size = uploadGameStoreRequest.FormFile.Length;

                var httpResponseDto = new HttpResponseDto<UploadGameStoreDto>("", StatusCodes.Status200OK);
                _logger.LogInformation("Done UploadGameStore {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<UploadGameStoreDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled UploadGameStore {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<UploadGameStoreDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error UploadGameStore {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
