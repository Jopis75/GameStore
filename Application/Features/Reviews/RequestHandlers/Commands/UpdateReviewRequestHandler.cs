using Application.Dtos.Common;
using Application.Dtos.Reviews;
using Application.Features.Reviews.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Features.Reviews.RequestHandlers.Commands
{
    public class UpdateReviewRequestHandler : IRequestHandler<UpdateReviewRequest, HttpResponseDto<UpdateReviewResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<UpdateReviewRequestDto> _validator;

        private readonly ILogger<UpdateReviewRequestHandler> _logger;

        public UpdateReviewRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateReviewRequestDto> validator, ILogger<UpdateReviewRequestHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<UpdateReviewResponseDto>> Handle(UpdateReviewRequest updateReviewRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin UpdateReview {@UpdateReviewRequest}.", updateReviewRequest);

                if (updateReviewRequest.UpdateReviewRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<UpdateReviewResponseDto>(new ArgumentNullException(nameof(updateReviewRequest.UpdateReviewRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UpdateReview {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(updateReviewRequest.UpdateReviewRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var httpResponseDto1 = new HttpResponseDto<UpdateReviewResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UpdateReview {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var review = await _unitOfWork.ReviewRepository.ReadByIdAsync(updateReviewRequest.UpdateReviewRequestDto.Id);
                _mapper.Map(updateReviewRequest.UpdateReviewRequestDto, review);
                var updatedReview = await _unitOfWork.ReviewRepository.UpdateAsync(review);
                await _unitOfWork.SaveAsync();

                var httpResponseDto = new HttpResponseDto<UpdateReviewResponseDto>(new UpdateReviewResponseDto
                {
                    Id = updatedReview.Id,
                    UpdatedAt = updatedReview.UpdatedAt,
                    UpdatedBy = updatedReview.UpdatedBy
                }, StatusCodes.Status200OK);
                _logger.LogInformation("End UpdateReview {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<UpdateReviewResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error UpdateReview {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
