using Application.Dtos.General;
using Application.Features.Reviews.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Reviews.RequestHandlers.Commands
{
    public class UpdateReviewRequestHandler : IRequestHandler<UpdateReviewRequest, HttpResponseDto<ReviewDto>>
    {
        private readonly IReviewRepository _reviewRepository;

        private readonly IMapper _mapper;

        private readonly IValidator<UpdateReviewRequest> _validator;

        private readonly ILogger<UpdateReviewRequestHandler> _logger;

        public UpdateReviewRequestHandler(IReviewRepository reviewRepository, IMapper mapper, IValidator<UpdateReviewRequest> validator, ILogger<UpdateReviewRequestHandler> logger)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<HttpResponseDto<ReviewDto>> Handle(UpdateReviewRequest updateReviewRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin UpdateReview {@UpdateReviewRequest}.", updateReviewRequest);

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

                var reviewDto = _mapper.Map<ReviewDto>(updateReviewRequest);
                var updatedReviewDto = await _reviewRepository.UpdateAsync(reviewDto, cancellationToken);

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
