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
    public class ReadReviewByIdRequestHandler(IUnitOfWork unitOfWork, IValidator<ReadReviewByIdRequest> validator, ILogger<ReadReviewByIdRequestHandler> logger) : IRequestHandler<ReadReviewByIdRequest, HttpResponseDto<ReviewDto>>
    {
        public async Task<HttpResponseDto<ReviewDto>> Handle(ReadReviewByIdRequest readReviewByIdRequest, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Begin ReadReviewById {@ReadReviewByIdRequest}.", readReviewByIdRequest);

                if (readReviewByIdRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(readReviewByIdRequest));
                    var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error ReadReviewById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await validator.ValidateAsync(readReviewByIdRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status400BadRequest);
                    logger.LogError(ex, "Error ReadReviewById {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var reviewDto = await unitOfWork.ReviewRepository.ReadByIdAsync(readReviewByIdRequest.Id, cancellationToken);

                var httpResponseDto = new HttpResponseDto<ReviewDto>(reviewDto, StatusCodes.Status200OK);
                logger.LogInformation("Done ReadReviewById {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Canceled ReadReviewById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status500InternalServerError);
                logger.LogError(ex, "Error ReadReviewById {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
