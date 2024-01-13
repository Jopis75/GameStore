using Application.Dtos.Reviews;
using FluentValidation;

namespace Application.Validators.Reviews
{
    public class ReadReviewByVideoGameIdRequestDtoValidator : AbstractValidator<ReadReviewByVideoGameIdRequestDto>
    {
        public ReadReviewByVideoGameIdRequestDtoValidator()
        {
            RuleFor(readByVideoGameIdRequestDto => readByVideoGameIdRequestDto.VideoGameId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
