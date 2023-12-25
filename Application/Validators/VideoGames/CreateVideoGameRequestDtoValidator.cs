using Application.Dtos.VideoGames;
using FluentValidation;

namespace Application.Validators.VideoGames
{
    public class CreateVideoGameRequestDtoValidator : AbstractValidator<CreateVideoGameRequestDto>
    {
        public CreateVideoGameRequestDtoValidator()
        {
            RuleFor(createVideoGameRequestDto => createVideoGameRequestDto.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createVideoGameRequestDto => createVideoGameRequestDto.DeveloperId)
                .NotEqual(0)
                .WithMessage("{PropertyName} must not equal 0.");

            RuleFor(createVideoGameRequestDto => createVideoGameRequestDto.ReleaseDate)
                .LessThanOrEqualTo(createVideoGameRequestDto => createVideoGameRequestDto.PurchaseDate)
                .WithMessage("{PropertyName} must be less than or equal to {ComparisonProperty}.");

            RuleFor(createVideoGameRequestDto => createVideoGameRequestDto.PurchaseDate)
                .GreaterThanOrEqualTo(createVideoGameRequestDto => createVideoGameRequestDto.ReleaseDate)
                .WithMessage("{PropertyName} must be greater than or equal to {ComparisonProperty}.");

            RuleFor(createVideoGameRequestDto => createVideoGameRequestDto.Price)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(createVideoGameRequestDto => createVideoGameRequestDto.TotalTimePlayed)
                .GreaterThanOrEqualTo(TimeSpan.Zero)
                .WithMessage("{PropertyName} must be greater than or equal to " + $"{TimeSpan.Zero}.");
        }
    }
}
