using Domain.Dtos;
using FluentValidation;

namespace Application.Validators.Dtos
{
    public class CreateReviewRequestDtoValidator : AbstractValidator<ReviewDto>
    {
        public CreateReviewRequestDtoValidator()
        {
            RuleFor(reviewDto => reviewDto.Grade)
                .GreaterThanOrEqualTo(0)
                .WithMessage("{PropertyName} must be greater than or equal to 0.")
                .LessThanOrEqualTo(100)
                .WithMessage("{PropertyName} must be less than or equal to 100.");

            RuleFor(reviewDto => reviewDto.VideoGameId)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(reviewDto => reviewDto.ReviewText)
                .NotNull()
                .NotEmpty()
                .WithMessage("{ProperyName} is required.");

            RuleFor(reviewDto => reviewDto.ReviewDate)
                .NotNull()
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("{PropertyName} must be less than or equal to " + $"{DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss")}.");
        }
    }
}
