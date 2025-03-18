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
    public class ReadMostPlayedVideoGameByConsoleIdRequestHandler : IRequestHandler<ReadMostPlayedVideoGameByConsoleIdRequest, HttpResponseDto<VideoGameDto>>
    {
        private readonly IVideoGameRepository _videoGameRepository;

        private readonly IValidator<ReadMostPlayedVideoGameByConsoleIdRequest> _validator;

        private readonly ILogger<ReadMostPlayedVideoGameByConsoleIdRequestHandler> _logger;

        public ReadMostPlayedVideoGameByConsoleIdRequestHandler(IVideoGameRepository videoGameRepository, IValidator<ReadMostPlayedVideoGameByConsoleIdRequest> validator, ILogger<ReadMostPlayedVideoGameByConsoleIdRequestHandler> logger)
        {
            _videoGameRepository = videoGameRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<HttpResponseDto<VideoGameDto>> Handle(ReadMostPlayedVideoGameByConsoleIdRequest readMostPlayedVideoGameByConsoleIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadMostPlayedVideoGameByConsoleId {@ReadMostPlayedVideoGameByConsoleIdRequest}.", readMostPlayedVideoGameByConsoleIdRequest);

                if (readMostPlayedVideoGameByConsoleIdRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(readMostPlayedVideoGameByConsoleIdRequest));
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error ReadMostPlayedVideoGameByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readMostPlayedVideoGameByConsoleIdRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error ReadMostPlayedVideoGameByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var videoGameDto = await _videoGameRepository.ReadMostPlayedByConsoleIdAsync(readMostPlayedVideoGameByConsoleIdRequest.ConsoleId, cancellationToken);

                var httpResponseDto = new HttpResponseDto<VideoGameDto>(videoGameDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadMostPlayedVideoGameByConsoleId {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Canceled ReadMostPlayedVideoGameByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error ReadMostPlayedVideoGameByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
