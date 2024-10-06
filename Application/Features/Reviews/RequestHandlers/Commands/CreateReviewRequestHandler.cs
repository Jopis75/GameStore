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
    public class CreateReviewRequestHandler : IRequestHandler<CreateReviewRequest, HttpResponseDto<ReviewDto>>
    {
        private readonly IReviewRepository _reviewRepository;

        private readonly IMapper _mapper;

        private readonly IValidator<CreateReviewRequest> _validator;

        private readonly ILogger<CreateReviewRequestHandler> _logger;

        public CreateReviewRequestHandler(IReviewRepository reviewRepository, IMapper mapper, IValidator<CreateReviewRequest> validator, ILogger<CreateReviewRequestHandler> logger)
        {
            _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<ReviewDto>> Handle(CreateReviewRequest createReviewRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin CreateReview {@CreateReviewRequest}.", createReviewRequest);

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

                var reviewDto = _mapper.Map<ReviewDto>(createReviewRequest);
                var createdReviewDto = await _reviewRepository.CreateAsync(reviewDto, cancellationToken);

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
