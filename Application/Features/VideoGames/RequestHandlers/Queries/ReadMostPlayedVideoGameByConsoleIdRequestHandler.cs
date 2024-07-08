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
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<ReadMostPlayedVideoGameByConsoleIdRequest> _validator;

        private readonly ILogger<ReadMostPlayedVideoGameByConsoleIdRequestHandler> _logger;

        public ReadMostPlayedVideoGameByConsoleIdRequestHandler(IUnitOfWork unitOfWork, IValidator<ReadMostPlayedVideoGameByConsoleIdRequest> validator, ILogger<ReadMostPlayedVideoGameByConsoleIdRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<VideoGameDto>> Handle(ReadMostPlayedVideoGameByConsoleIdRequest readMostPlayedVideoGameByConsoleIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadMostPlayedVideoGameByConsoleId {@ReadMostPlayedVideoGameByConsoleIdRequest}.", readMostPlayedVideoGameByConsoleIdRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (readMostPlayedVideoGameByConsoleIdRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(new ArgumentNullException(nameof(readMostPlayedVideoGameByConsoleIdRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadMostPlayedVideoGameByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readMostPlayedVideoGameByConsoleIdRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadMostPlayedVideoGameByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var videoGameDto = await _unitOfWork.VideoGameRepository.ReadMostPlayedByConsoleIdAsync(readMostPlayedVideoGameByConsoleIdRequest.ConsoleId, cancellationToken);

                var httpResponseDto = new HttpResponseDto<VideoGameDto>(videoGameDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadMostPlayedVideoGameByConsoleId {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled ReadMostPlayedVideoGameByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadMostPlayedVideoGameByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
