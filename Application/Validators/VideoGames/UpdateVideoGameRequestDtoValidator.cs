using Application.Dtos.VideoGames;
using Application.Validators.Common;
using FluentValidation;

namespace Application.Validators.VideoGames
{
    public class UpdateVideoGameRequestDtoValidator : AbstractValidator<UpdateVideoGameRequestDto>
    {
        public UpdateVideoGameRequestDtoValidator()
        {
            Include(new UpdateRequestDtoValidator());

            RuleFor(updateVideoGameRequestDto => updateVideoGameRequestDto.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(updateVideoGameRequestDto => updateVideoGameRequestDto.DeveloperId)
                .NotEqual(0)
                .WithMessage("{PropertyName} must not equal 0.");

            RuleFor(updateVideoGameRequestDto => updateVideoGameRequestDto.ReleaseDate)
                .LessThanOrEqualTo(updateVideoGameRequestDto => updateVideoGameRequestDto.PurchaseDate)
                .WithMessage("{PropertyName} must be less than or equal to {ComparisonProperty}.");

            RuleFor(updateVideoGameRequestDto => updateVideoGameRequestDto.PurchaseDate)
                .GreaterThanOrEqualTo(updateVideoGameRequestDto => updateVideoGameRequestDto.ReleaseDate)
                .WithMessage("{PropertyName} must be greater than or equal to {ComparisonProperty}.");

            RuleFor(updateVideoGameRequestDto => updateVideoGameRequestDto.Price)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(updateVideoGameRequestDto => updateVideoGameRequestDto.TotalTimePlayed)
                .GreaterThanOrEqualTo(TimeSpan.Zero)
                .WithMessage("{PropertyName} must be greater than or equal to " + $"{TimeSpan.Zero}.");
        }
    }
}
