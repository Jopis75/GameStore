using Application.Dtos.Common.Validators;
using FluentValidation;

namespace Application.Dtos.Reviews.Validators
{
    public class DeleteReviewRequestDtoValidator : AbstractValidator<DeleteReviewRequestDto>
    {
        public DeleteReviewRequestDtoValidator()
        {
            Include(new DeleteRequestDtoValidator());
        }
    }
}
