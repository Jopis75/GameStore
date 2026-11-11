using FluentValidation;
using GameStore.Application.Dtos.General;
using GameStore.Application.Exceptions;
using GameStore.Application.Features.VideoGames.Requests.Queries;
using GameStore.Application.Interfaces.Persistance;
using GameStore.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace GameStore.Application.Features.VideoGames.RequestHandlers.Queries
{
    public class ReadMostPlayedVideoGameByConsoleIdRequestHandler(IUnitOfWork unitOfWork, IValidator<ReadMostPlayedVideoGameByConsoleIdRequest> validator, ILogger<ReadMostPlayedVideoGameByConsoleIdRequestHandler> logger) : IRequestHandler<ReadMostPlayedVideoGameByConsoleIdRequest, HttpResponseDto<VideoGameDto>>
    {
        public async Task<HttpResponseDto<VideoGameDto>> Handle(ReadMostPlayedVideoGameByConsoleIdRequest readMostPlayedVideoGameByConsoleIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Begin ReadMostPlayedVideoGameByConsoleId {@ReadMostPlayedVideoGameByConsoleIdRequest}.", readMostPlayedVideoGameByConsoleIdRequest);

                if (readMostPlayedVideoGameByConsoleIdRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(readMostPlayedVideoGameByConsoleIdRequest));
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error ReadMostPlayedVideoGameByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await validator.ValidateAsync(readMostPlayedVideoGameByConsoleIdRequest, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error ReadMostPlayedVideoGameByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var videoGameDto = await unitOfWork.VideoGameRepository.ReadMostPlayedByConsoleIdAsync(readMostPlayedVideoGameByConsoleIdRequest.ConsoleId, cancellationToken);

                if (videoGameDto.IsNullObject)
                {
                    var ex = new NotFoundException($"Could not find VideoGame object with ConsoleId {readMostPlayedVideoGameByConsoleIdRequest.ConsoleId}.");
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status404NotFound);
                    logger.LogError(ex, "Error ReadMostPlayedVideoGameByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var httpResponseDto = new HttpResponseDto<VideoGameDto>(videoGameDto, StatusCodes.Status200OK);
                logger.LogInformation("Done ReadMostPlayedVideoGameByConsoleId {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled ReadMostPlayedVideoGameByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error ReadMostPlayedVideoGameByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
