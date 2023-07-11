using Application.Dtos.Reviews;
using Application.Validators.Common;
using FluentValidation;

namespace Application.Validators.Reviews
{
    public class DeleteReviewRequestDtoValidator : AbstractValidator<DeleteReviewRequestDto>
    {
        public DeleteReviewRequestDtoValidator()
        {
            Include(new DeleteRequestDtoValidator());
        }
    }
}
