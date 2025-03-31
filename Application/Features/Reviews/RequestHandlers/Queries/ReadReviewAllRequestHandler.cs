using Application.Dtos.General;
using Application.Features.Reviews.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Reviews.RequestHandlers.Queries
{
    public class ReadReviewAllRequestHandler(IUnitOfWork unitOfWork, ILogger<ReadReviewAllRequestHandler> logger) : IRequestHandler<ReadReviewAllRequest, HttpResponseDto<ReviewDto>>
    {
        public async Task<HttpResponseDto<ReviewDto>> Handle(ReadReviewAllRequest readReviewAllRequest, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Begin ReadReviewAll {@ReadReviewAllRequest}.", readReviewAllRequest);

                if (readReviewAllRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(readReviewAllRequest));
                    var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error ReadReviewAll {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var reviewDtos = await unitOfWork.ReviewRepository.ReadAllAsync(cancellationToken);

                var httpResponseDto = new HttpResponseDto<ReviewDto>(reviewDtos.ToArray(), StatusCodes.Status200OK);
                logger.LogInformation("Done ReadReviewAll {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled ReadReviewAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error ReadReviewAll {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
