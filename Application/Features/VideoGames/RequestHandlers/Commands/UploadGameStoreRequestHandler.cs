using Application.Dtos.General;
using Application.Features.VideoGames.Requests.Commands;
using Application.Interfaces.Persistance;
using FluentValidation;
using MediatR;
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

        public Task<HttpResponseDto<UploadGameStoreDto>> Handle(UploadGameStoreRequest uploadGameStoreRequest, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
