using Application.Dtos.Common;
using Application.Dtos.Reviews;
using Application.Features.Reviews.Requests.Commands;
using Application.Interfaces.Persistance;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Reviews.RequestHandlers.Commands
{
    public class UpdateReviewRequestHandler : IRequestHandler<UpdateReviewRequest, HttpResponseDto<UpdateReviewResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly IValidator<UpdateReviewRequestDto> _validator;

        public UpdateReviewRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateReviewRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<UpdateReviewResponseDto>> Handle(UpdateReviewRequest updateReviewRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (updateReviewRequest.UpdateReviewRequestDto == null)
                {
                    return new HttpResponseDto<UpdateReviewResponseDto>(new ArgumentNullException(nameof(updateReviewRequest.UpdateReviewRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(updateReviewRequest.UpdateReviewRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<UpdateReviewResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var review = await _unitOfWork.ReviewRepository.ReadByIdAsync(updateReviewRequest.UpdateReviewRequestDto.Id);
                _mapper.Map(updateReviewRequest.UpdateReviewRequestDto, review);
                review = await _unitOfWork.ReviewRepository.UpdateAsync(review);

                return new HttpResponseDto<UpdateReviewResponseDto>(new UpdateReviewResponseDto
                {
                    Id = review.Id,
                    UpdatedAt = review.UpdatedAt,
                    UpdatedBy = string.Empty
                }, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<UpdateReviewResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
