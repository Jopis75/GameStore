using FluentValidation;
using GameStore.Application.Features.Reviews.Requests.Commands;

namespace GameStore.Application.Validators.Requests.Reviews.Commands
{
    public class UpdateReviewRequestValidator : AbstractValidator<UpdateReviewRequest>
    {
        public UpdateReviewRequestValidator()
        {
            RuleFor(updateReviewRequest => updateReviewRequest.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(updateReviewRequest => updateReviewRequest.Grade)
                .GreaterThanOrEqualTo(0)
                .WithMessage("{PropertyName} must be greater than or equal to 0.")
                .LessThanOrEqualTo(100)
                .WithMessage("{PropertyName} must be less than or equal to 100.");

            RuleFor(updateReviewRequest => updateReviewRequest.VideoGameId)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(updateReviewRequest => updateReviewRequest.ReviewText)
                .NotNull()
                .NotEmpty()
                .WithMessage("{ProperyName} is required.");

            RuleFor(updateReviewRequest => updateReviewRequest.ReviewDate)
                .NotNull()
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("{PropertyName} must be less than or equal to " + $"{DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss")}.");
        }
    }
}
