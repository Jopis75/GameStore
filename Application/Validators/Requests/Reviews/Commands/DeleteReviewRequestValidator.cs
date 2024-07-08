using Application.Features.Reviews.Requests.Commands;
using FluentValidation;

namespace Application.Validators.Requests.Reviews.Commands
{
    public class DeleteReviewRequestValidator : AbstractValidator<DeleteReviewRequest>
    {
        public DeleteReviewRequestValidator()
        {
            RuleFor(deleteReviewRequest => deleteReviewRequest.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
