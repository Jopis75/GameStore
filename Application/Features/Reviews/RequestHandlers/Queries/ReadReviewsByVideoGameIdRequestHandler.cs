using Application.Dtos.General;
using Application.Features.Reviews.Requests.Queries;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Reviews.RequestHandlers.Queries
{
    public class ReadReviewsByVideoGameIdRequestHandler(IUnitOfWork unitOfWork, IValidator<ReadReviewsByVideoGameIdRequest> validator, ILogger<ReadReviewsByVideoGameIdRequestHandler> logger) : IRequestHandler<ReadReviewsByVideoGameIdRequest, HttpResponseDto<ReviewDto>>
    {
        public async Task<HttpResponseDto<ReviewDto>> Handle(ReadReviewsByVideoGameIdRequest readReviewsByVideoGameIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Begin ReadReviewsByVideoGameId {@ReadReviewsByVideoGameIdRequest}.", readReviewsByVideoGameIdRequest);

                if (readReviewsByVideoGameIdRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(readReviewsByVideoGameIdRequest));
                    var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error ReadReviewsByVideoGameId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await validator.ValidateAsync(readReviewsByVideoGameIdRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error ReadReviewsByVideoGameId {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var reviewDtos = await unitOfWork.ReviewRepository.ReadByVideoGameIdAsync(readReviewsByVideoGameIdRequest.VideoGameId, cancellationToken);

                var httpResponseDto = new HttpResponseDto<ReviewDto>(reviewDtos.ToArray(), StatusCodes.Status200OK);
                logger.LogInformation("Done ReadReviewsByVideoGameId {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled ReadReviewsByVideoGameId {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error ReadReviewsByVideoGameId {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
