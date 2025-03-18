using Application.Dtos.General;
using Application.Features.Reviews.Requests.Commands;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Reviews.RequestHandlers.Commands
{
    public class DeleteReviewRequestHandler : IRequestHandler<DeleteReviewRequest, HttpResponseDto<ReviewDto>>
    {
        private readonly IReviewRepository _reviewRepository;

        private readonly IValidator<DeleteReviewRequest> _validator;

        private readonly ILogger<DeleteReviewRequestHandler> _logger;

        public DeleteReviewRequestHandler(IReviewRepository reviewRepository, IValidator<DeleteReviewRequest> validator, ILogger<DeleteReviewRequestHandler> logger)
        {
            _reviewRepository = reviewRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<HttpResponseDto<ReviewDto>> Handle(DeleteReviewRequest deleteReviewRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin DeleteReview {@DeleteReviewRequest}.", deleteReviewRequest);

                if (deleteReviewRequest == null)
                {
                    var ex = new ArgumentNullException(nameof(deleteReviewRequest));
                    var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error DeleteReview {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(deleteReviewRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var ex = new ValidationException(validationResult.Errors);
                    var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error DeleteReview {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var deletedReviewDto = await _reviewRepository.DeleteByIdAsync(deleteReviewRequest.Id, cancellationToken);

                var httpResponseDto = new HttpResponseDto<ReviewDto>(deletedReviewDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done DeleteReview {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Canceled DeleteReview {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error DeleteReview {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
