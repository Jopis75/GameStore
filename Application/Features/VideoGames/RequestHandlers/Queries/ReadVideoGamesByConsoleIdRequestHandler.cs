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
    public class ReadVideoGamesByConsoleIdRequestHandler : IRequestHandler<ReadVideoGamesByConsoleIdRequest, HttpResponseDto<List<VideoGameDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<ReadVideoGamesByConsoleIdRequest> _validator;

        private readonly ILogger<ReadVideoGamesByConsoleIdRequestHandler> _logger;

        public ReadVideoGamesByConsoleIdRequestHandler(IUnitOfWork unitOfWork, IValidator<ReadVideoGamesByConsoleIdRequest> validator, ILogger<ReadVideoGamesByConsoleIdRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<List<VideoGameDto>>> Handle(ReadVideoGamesByConsoleIdRequest readVideoGamesByConsoleIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadVideoGamesByConsoleId {@ReadVideoGamesByConsoleIdRequest}.", readVideoGamesByConsoleIdRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (readVideoGamesByConsoleIdRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<List<VideoGameDto>>(new ArgumentNullException(nameof(readVideoGamesByConsoleIdRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadVideoGamesByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readVideoGamesByConsoleIdRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<List<VideoGameDto>>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadVideoGamesByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var videoGameDtos = await _unitOfWork.VideoGameRepository.ReadByConsoleIdAsync(readVideoGamesByConsoleIdRequest.ConsoleId, cancellationToken);

                var httpResponseDto = new HttpResponseDto<List<VideoGameDto>>(videoGameDtos.ToList(), StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadVideoGamesByConsoleId {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<List<VideoGameDto>>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled ReadVideoGamesByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<List<VideoGameDto>>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadVideoGamesByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
