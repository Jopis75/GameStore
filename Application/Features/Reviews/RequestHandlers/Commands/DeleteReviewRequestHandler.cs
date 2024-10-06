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
            _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ReviewDto>> Handle(DeleteReviewRequest deleteReviewRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin DeleteReview {@DeleteReviewRequest}.", deleteReviewRequest);

                if (deleteReviewRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReviewDto>(new ArgumentNullException(nameof(deleteReviewRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error DeleteReview {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(deleteReviewRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReviewDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error DeleteReview {@HttpResponseDto}.", httpResponseDto1);
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
                _logger.LogError("Canceled DeleteReview {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error DeleteReview {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
