using Application.Dtos.General;
using Application.Features.VideoGames.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.VideoGames.RequestHandlers.Commands
{
    public class CreateVideoGameRequestHandler : IRequestHandler<CreateVideoGameRequest, HttpResponseDto<VideoGameDto>>
    {
        private readonly IVideoGameRepository _videoGameRepository;

        private readonly IMapper _mapper;

        private readonly IValidator<CreateVideoGameRequest> _validator;

        private readonly ILogger<CreateVideoGameRequestHandler> _logger;

        public CreateVideoGameRequestHandler(IVideoGameRepository videoGameRepository, IMapper mapper, IValidator<CreateVideoGameRequest> validator, ILogger<CreateVideoGameRequestHandler> logger)
        {
            _videoGameRepository = videoGameRepository ?? throw new ArgumentNullException(nameof(videoGameRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<VideoGameDto>> Handle(CreateVideoGameRequest createVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin CreateVideoGame {@CreateVideoGameRequest}.", createVideoGameRequest);

                if (createVideoGameRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(new ArgumentNullException(nameof(createVideoGameRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(createVideoGameRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var videoGameDto = _mapper.Map<VideoGameDto>(createVideoGameRequest);
                var createdVideoGameDto = await _videoGameRepository.CreateAsync(videoGameDto, cancellationToken);

                var httpResponseDto = new HttpResponseDto<VideoGameDto>(createdVideoGameDto, StatusCodes.Status201Created);
                _logger.LogInformation("Done CreateVideoGame {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled CreateVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error CreateVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
