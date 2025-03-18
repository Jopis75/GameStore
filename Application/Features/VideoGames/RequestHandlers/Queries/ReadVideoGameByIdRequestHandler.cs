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
    public class ReadVideoGameByIdRequestHandler : IRequestHandler<ReadVideoGameByIdRequest, HttpResponseDto<VideoGameDto>>
    {
        private readonly IVideoGameRepository _videoGameRepository;

        private readonly IValidator<ReadVideoGameByIdRequest> _validator;

        private readonly ILogger<ReadVideoGameByIdRequestHandler> _logger;

        public ReadVideoGameByIdRequestHandler(IVideoGameRepository videoGameRepository, IValidator<ReadVideoGameByIdRequest> validator, ILogger<ReadVideoGameByIdRequestHandler> logger)
        {
            _videoGameRepository = videoGameRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<HttpResponseDto<VideoGameDto>> Handle(ReadVideoGameByIdRequest readVideoGameByIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadVideoGameById {@ReadVideoGameByIdRequest}.", readVideoGameByIdRequest);

                if (readVideoGameByIdRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(readVideoGameByIdRequest));
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error ReadVideoGameById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readVideoGameByIdRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error ReadVideoGameById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var videoGameDto = await _videoGameRepository.ReadByIdAsync(readVideoGameByIdRequest.Id, cancellationToken);

                var httpResponseDto = new HttpResponseDto<VideoGameDto>(videoGameDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadVideoGameById {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Canceled ReadVideoGameById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error ReadVideoGameById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
