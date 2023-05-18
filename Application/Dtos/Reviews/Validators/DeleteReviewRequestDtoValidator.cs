using FluentValidation;

namespace Application.Dtos.Reviews.Validators
{
    public class DeleteReviewRequestDtoValidator : AbstractValidator<DeleteReviewRequestDto>
    {
        DeleteReviewRequestDtoValidator()
        {
            Include(new DeleteReviewRequestDtoValidator());
        }
    }
}
