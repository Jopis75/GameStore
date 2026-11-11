using FluentValidation;
using GameStore.Application.Dtos.General;
using GameStore.Application.Features.VideoGames.Requests.Queries;
using GameStore.Application.Interfaces.Persistance;
using GameStore.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace GameStore.Application.Features.VideoGames.RequestHandlers.Queries
{
    public class ReadVideoGamesByConsoleIdRequestHandler(IUnitOfWork unitOfWork, IValidator<ReadVideoGamesByConsoleIdRequest> validator, ILogger<ReadVideoGamesByConsoleIdRequestHandler> logger) : IRequestHandler<ReadVideoGamesByConsoleIdRequest, HttpResponseDto<VideoGameDto>>
    {
        public async Task<HttpResponseDto<VideoGameDto>> Handle(ReadVideoGamesByConsoleIdRequest readVideoGamesByConsoleIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Begin ReadVideoGamesByConsoleId {@ReadVideoGamesByConsoleIdRequest}.", readVideoGamesByConsoleIdRequest);

                if (readVideoGamesByConsoleIdRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(readVideoGamesByConsoleIdRequest));
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error ReadVideoGamesByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await validator.ValidateAsync(readVideoGamesByConsoleIdRequest, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error ReadVideoGamesByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var videoGameDtos = await unitOfWork.VideoGameRepository.ReadByConsoleIdAsync(readVideoGamesByConsoleIdRequest.ConsoleId, cancellationToken);

                var httpResponseDto = new HttpResponseDto<VideoGameDto>(videoGameDtos.ToArray(), videoGameDtos.Any()
                    ? StatusCodes.Status200OK
                    : StatusCodes.Status204NoContent);
                logger.LogInformation("Done ReadVideoGamesByConsoleId {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled ReadVideoGamesByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error ReadVideoGamesByConsoleId {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
