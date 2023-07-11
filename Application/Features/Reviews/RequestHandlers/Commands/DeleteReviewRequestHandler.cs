using Application.Dtos.Common;
using Application.Dtos.Reviews;
using Application.Features.Reviews.Requests.Commands;
using Application.Interfaces.Persistance;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Reviews.RequestHandlers.Commands
{
    public class DeleteReviewRequestHandler : IRequestHandler<DeleteReviewRequest, HttpResponseDto<DeleteReviewResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<DeleteReviewRequestDto> _validator;

        public DeleteReviewRequestHandler(IUnitOfWork unitOfWork, IValidator<DeleteReviewRequestDto> validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<HttpResponseDto<DeleteReviewResponseDto>> Handle(DeleteReviewRequest deleteReviewRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (deleteReviewRequest.DeleteReviewRequestDto == null)
                {
                    return new HttpResponseDto<DeleteReviewResponseDto>(new ArgumentNullException(nameof(deleteReviewRequest.DeleteReviewRequestDto)).Message, StatusCodes.Status400BadRequest);
                }

                var validationResult = await _validator.ValidateAsync(deleteReviewRequest.DeleteReviewRequestDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return new HttpResponseDto<DeleteReviewResponseDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                }

                var review = await _unitOfWork.ReviewRepository.ReadByIdAsync(deleteReviewRequest.DeleteReviewRequestDto.Id);
                var deletedReview = await _unitOfWork.ReviewRepository.DeleteAsync(review);
                await _unitOfWork.SaveAsync();

                return new HttpResponseDto<DeleteReviewResponseDto>(new DeleteReviewResponseDto
                {
                    Id = deletedReview.Id,
                    DeletedAt = deletedReview.DeletedAt,
                    DeletedBy = string.Empty
                }, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseDto<DeleteReviewResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
