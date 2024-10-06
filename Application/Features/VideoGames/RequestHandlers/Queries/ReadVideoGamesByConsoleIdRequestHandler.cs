using Application.Dtos.General;
using Application.Features.VideoGames.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.VideoGames.RequestHandlers.Queries
{
    public class ReadVideoGamesByConsoleIdRequestHandler : IRequestHandler<ReadVideoGamesByConsoleIdRequest, HttpResponseDto<VideoGameDto>>
    {
        private readonly IVideoGameRepository _videoGameRepository;

        private readonly IValidator<ReadVideoGamesByConsoleIdRequest> _validator;

        private readonly ILogger<ReadVideoGamesByConsoleIdRequestHandler> _logger;

        public ReadVideoGamesByConsoleIdRequestHandler(IVideoGameRepository videoGameRepository, IValidator<ReadVideoGamesByConsoleIdRequest> validator, ILogger<ReadVideoGamesByConsoleIdRequestHandler> logger)
        {
            _videoGameRepository = videoGameRepository ?? throw new ArgumentNullException(nameof(videoGameRepository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<VideoGameDto>> Handle(ReadVideoGamesByConsoleIdRequest readVideoGamesByConsoleIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadVideoGamesByConsoleId {@ReadVideoGamesByConsoleIdRequest}.", readVideoGamesByConsoleIdRequest);

                if (readVideoGamesByConsoleIdRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(new ArgumentNullException(nameof(readVideoGamesByConsoleIdRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadVideoGamesByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readVideoGamesByConsoleIdRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadVideoGamesByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var videoGameDtos = await _videoGameRepository.ReadByConsoleIdAsync(readVideoGamesByConsoleIdRequest.ConsoleId, cancellationToken);

                var httpResponseDto = new HttpResponseDto<VideoGameDto>(videoGameDtos.ToArray(), StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadVideoGamesByConsoleId {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled ReadVideoGamesByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadVideoGamesByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
