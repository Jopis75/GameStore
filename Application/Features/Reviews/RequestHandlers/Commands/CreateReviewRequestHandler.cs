using Application.Dtos.Common;
using Application.Dtos.Reviews;
using Application.Features.Reviews.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Reviews.RequestHandlers.Commands
{
    public class CreateReviewRequestHandler : IRequestHandler<CreateReviewRequest, HttpResponseDto<CreateReviewResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<CreateReviewRequestDto> _validator;

        private readonly ILogger<CreateReviewRequestHandler> _logger;

        public CreateReviewRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateReviewRequestDto> validator, ILogger<CreateReviewRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<CreateReviewResponseDto>> Handle(CreateReviewRequest createReviewRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin CreateReview {@CreateReviewRequest}.", createReviewRequest);

                if (createReviewRequest.CreateReviewRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<CreateReviewResponseDto>(new ArgumentNullException(nameof(createReviewRequest.CreateReviewRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateReview {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(createReviewRequest.CreateReviewRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<CreateReviewResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateReview {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var review = _mapper.Map<Review>(createReviewRequest.CreateReviewRequestDto);
                var createdReview = await _unitOfWork.ReviewRepository.CreateAsync(review);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<CreateReviewResponseDto>(new CreateReviewResponseDto
                {
                    Id = createdReview.Id,
                    CreatedAt = createdReview.CreatedAt,
                    CreatedBy = createdReview.CreatedBy
                }, StatusCodes.Status201Created);
                _logger.LogInformation("Done CreateReview {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<CreateReviewResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error CreateReview {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
