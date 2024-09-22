using Application.Features.Reviews.Requests.Commands;
using FluentValidation;

namespace Application.Validators.Requests.Reviews.Commands
{
    public class CreateReviewRequestValidator : AbstractValidator<CreateReviewRequest>
    {
        public CreateReviewRequestValidator()
        {
            RuleFor(createReviewRequest => createReviewRequest.Grade)
                .GreaterThanOrEqualTo(0)
                .WithMessage("{PropertyName} must be greater than or equal to 0.")
                .LessThanOrEqualTo(100)
                .WithMessage("{PropertyName} must be less than or equal to 100.");

            RuleFor(createReviewRequest => createReviewRequest.VideoGameId)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(createReviewRequest => createReviewRequest.ReviewText)
                .NotNull()
                .NotEmpty()
                .WithMessage("{ProperyName} is required."   );

            RuleFor(createReviewRequest => createReviewRequest.ReviewDate)
                .NotNull()
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("{PropertyName} must be less than or equal to " + $"{DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss")}.");
        }
    }
}
