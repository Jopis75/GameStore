using Application.Dtos.Reviews;
using FluentValidation;

namespace Application.Validators.Reviews
{
    public class CreateReviewRequestDtoValidator : AbstractValidator<CreateReviewRequestDto>
    {
        public CreateReviewRequestDtoValidator()
        {
            RuleFor(createReviewRequestDto => createReviewRequestDto.Grade)
                .GreaterThanOrEqualTo(0)
                .WithMessage("{PropertyName} must be greater than or equal to 0.")
                .LessThanOrEqualTo(100)
                .WithMessage("{PropertyName} must be less than or equal to 100.");

            RuleFor(createReviewRequestDto => createReviewRequestDto.VideoGameId)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(createReviewRequestDto => createReviewRequestDto.ReviewText)
                .NotNull()
                .NotEmpty()
                .WithMessage("{ProperyName} is required.");

            RuleFor(createReviewRequestDto => createReviewRequestDto.ReviewDate)
                .NotNull()
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("{PropertyName} must be less than or equal to " + $"{DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss")}.");
        }
    }
}
