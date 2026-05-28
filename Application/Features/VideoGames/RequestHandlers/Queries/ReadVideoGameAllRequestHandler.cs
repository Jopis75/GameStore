using Application.Dtos.General;
using Application.Features.VideoGames.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.VideoGames.RequestHandlers.Queries
{
    public class ReadVideoGameAllRequestHandler(IUnitOfWork unitOfWork, ILogger<ReadVideoGameAllRequestHandler> logger) : IRequestHandler<ReadVideoGameAllRequest, HttpResponseDto<VideoGameDto>>
    {
        public async Task<HttpResponseDto<VideoGameDto>> Handle(ReadVideoGameAllRequest readVideoGameAllRequest, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Begin ReadVideoGameAll {@ReadVideoGameAllRequest}.", readVideoGameAllRequest);

                if (readVideoGameAllRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(readVideoGameAllRequest));
                    var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error ReadVideoGameAll {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var videoGameDtos = await unitOfWork.VideoGameRepository.ReadAllAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<VideoGameDto>(videoGameDtos.ToArray(), videoGameDtos.Any()
                    ? StatusCodes.Status200OK
                    : StatusCodes.Status204NoContent);
                logger.LogInformation("Done ReadVideoGameAll {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled ReadVideoGameAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<VideoGameDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error ReadVideoGameAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
