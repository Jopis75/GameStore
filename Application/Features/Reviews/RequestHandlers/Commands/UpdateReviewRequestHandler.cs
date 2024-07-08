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
    public class UpdateReviewRequestHandler : IRequestHandler<UpdateReviewRequest, HttpResponseDto<ReviewDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<UpdateReviewRequest> _validator;

        private readonly ILogger<UpdateReviewRequestHandler> _logger;

        public UpdateReviewRequestHandler(IUnitOfWork unitOfWork, IValidator<UpdateReviewRequest> validator, ILogger<UpdateReviewRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ReviewDto>> Handle(UpdateReviewRequest updateReviewRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin UpdateReview {@UpdateReviewRequest}.", updateReviewRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (updateReviewRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReviewDto>(new ArgumentNullException(nameof(updateReviewRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UpdateReview {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(updateReviewRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReviewDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UpdateReview {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var updatedReviewDto = await _unitOfWork.ReviewRepository.UpdateAsync(updateReviewRequest.ReviewDto, cancellationToken);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<ReviewDto>(updatedReviewDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done UpdateReview {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled UpdateReview {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error UpdateReview {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
