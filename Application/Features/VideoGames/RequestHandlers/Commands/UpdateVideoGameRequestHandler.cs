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
    public class UpdateVideoGameRequestHandler : IRequestHandler<UpdateVideoGameRequest, HttpResponseDto<VideoGameDto>>
    {
        private readonly IVideoGameRepository _videoGameRepository;

        private readonly IMapper _mapper;

        private readonly IValidator<UpdateVideoGameRequest> _validator;

        private readonly ILogger<UpdateVideoGameRequestHandler> _logger;

        public UpdateVideoGameRequestHandler(IVideoGameRepository videoGameRepository, IMapper mapper, IValidator<UpdateVideoGameRequest> validator, ILogger<UpdateVideoGameRequestHandler> logger)
        {
            _videoGameRepository = videoGameRepository;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<HttpResponseDto<VideoGameDto>> Handle(UpdateVideoGameRequest updateVideoGameRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin UpdateVideoGame {@UpdateVideoGameRequest}.", updateVideoGameRequest);

                if (updateVideoGameRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(updateVideoGameRequest));
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error UpdateVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(updateVideoGameRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error UpdateVideoGame {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var videoGameDto = _mapper.Map<VideoGameDto>(updateVideoGameRequest);
                var updatedVideoGameDto = await _videoGameRepository.UpdateAsync(videoGameDto, cancellationToken);

                var httpResponseDto = new HttpResponseDto<VideoGameDto>(updatedVideoGameDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done UpdateVideoGame {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Canceled UpdateVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error UpdateVideoGame {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
