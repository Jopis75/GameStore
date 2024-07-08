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
    public class CreateReviewRequestHandler : IRequestHandler<CreateReviewRequest, HttpResponseDto<ReviewDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<CreateReviewRequest> _validator;

        private readonly ILogger<CreateReviewRequestHandler> _logger;

        public CreateReviewRequestHandler(IUnitOfWork unitOfWork, IValidator<CreateReviewRequest> validator, ILogger<CreateReviewRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ReviewDto>> Handle(CreateReviewRequest createReviewRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin CreateReview {@CreateReviewRequest}.", createReviewRequest);

                cancellationToken.ThrowIfCancellationRequested();

                if (createReviewRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReviewDto>(new ArgumentNullException(nameof(createReviewRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateReview {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(createReviewRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<ReviewDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateReview {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var createdReviewDto = await _unitOfWork.ReviewRepository.CreateAsync(createReviewRequest.ReviewDto, cancellationToken);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<ReviewDto>(createdReviewDto, StatusCodes.Status201Created);
                _logger.LogInformation("Done CreateReview {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled CreateReview {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<ReviewDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error CreateReview {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
