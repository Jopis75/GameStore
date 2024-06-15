using Application.Dtos.Common;
using Application.Features.VideoGames.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.VideoGames.RequestHandlers.Queries
{
    public class ReadVideoGameByConsoleIdRequestHandler : IRequestHandler<ReadVideoGameByConsoleIdRequest, HttpResponseDto<List<VideoGameDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<VideoGameDto> _validator;

        private readonly ILogger<ReadVideoGameByConsoleIdRequestHandler> _logger;

        public ReadVideoGameByConsoleIdRequestHandler(IUnitOfWork unitOfWork, IValidator<VideoGameDto> validator, ILogger<ReadVideoGameByConsoleIdRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<List<VideoGameDto>>> Handle(ReadVideoGameByConsoleIdRequest readVideoGameByConsoleIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin ReadVideoGameByConsoleId {@ReadVideoGameByConsoleIdRequest}.", readVideoGameByConsoleIdRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (readVideoGameByConsoleIdRequest.ConsoleId == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<List<VideoGameDto>>(new ArgumentNullException(nameof(readVideoGameByConsoleIdRequest.ConsoleId)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadVideoGameByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(readVideoGameByConsoleIdRequest.ConsoleId, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<List<VideoGameDto>>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error ReadVideoGameByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var videoGameDtos = await _unitOfWork.VideoGameRepository.ReadByConsoleIdAsync(readVideoGameByConsoleIdRequest.ConsoleId ?? 0, true);

                var httpResponseDto = new HttpResponseDto<List<VideoGameDto>>(videoGameDtos.ToList(), StatusCodes.Status200OK);
                _logger.LogInformation("Done ReadVideoGameByConsoleId {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<List<VideoGameDto>>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled ReadVideoGameByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<List<VideoGameDto>>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error ReadVideoGameByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
