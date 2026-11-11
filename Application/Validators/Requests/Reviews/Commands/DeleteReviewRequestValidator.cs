using FluentValidation;
using GameStore.Application.Features.Reviews.Requests.Commands;

namespace GameStore.Application.Validators.Requests.Reviews.Commands
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
