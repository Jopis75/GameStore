using Application.Dtos.Reviews;
using Application.Validators.Common;
using FluentValidation;

namespace Application.Validators.Reviews
{
    public class UpdateReviewRequestDtoValidator : AbstractValidator<UpdateReviewRequestDto>
    {
        public UpdateReviewRequestDtoValidator()
        {
            Include(new UpdateRequestDtoValidator());

            RuleFor(createReviewRequestDto => createReviewRequestDto.Grade)
                .GreaterThanOrEqualTo(0)
                .WithMessage("{PropertyName} must be greater than or equal to 0.")
                .LessThanOrEqualTo(100)
                .WithMessage("{PropertyName} must be less than or equal to 100.");

            RuleFor(createReviewRequestDto => createReviewRequestDto.VideoGameId)
                .NotNull()
                .NotEqual(0)
                .WithMessage("{PropertyName} must not equal 0.");

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
