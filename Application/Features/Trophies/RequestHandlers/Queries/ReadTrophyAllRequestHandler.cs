using Application.Dtos.General;
using Application.Features.Trophies.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Trophies.RequestHandlers.Queries
{
    public class ReadTrophyAllRequestHandler(IUnitOfWork unitOfWork, ILogger<ReadTrophyAllRequestHandler> logger) : IRequestHandler<ReadTrophyAllRequest, HttpResponseDto<TrophyDto>>
    {
        public async Task<HttpResponseDto<TrophyDto>> Handle(ReadTrophyAllRequest readTrophyAllRequest, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Begin ReadTrophyAll {@ReadTrophyAllRequest}.", readTrophyAllRequest);

                if (readTrophyAllRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(readTrophyAllRequest));
                    var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error ReadTrophyAll {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var trophyDtos = await unitOfWork.TrophyRepository.ReadAllAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<TrophyDto>(trophyDtos.ToArray(), trophyDtos.Any()
                    ? StatusCodes.Status200OK
                    : StatusCodes.Status204NoContent);
                logger.LogInformation("Done ReadTrophyAll {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled ReadTrophyAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<TrophyDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error ReadTrophyAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
