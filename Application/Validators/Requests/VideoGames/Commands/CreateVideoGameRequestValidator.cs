using Application.Features.VideoGames.Requests.Commands;
using FluentValidation;

namespace Application.Validators.Requests.VideoGames.Commands
{
    public class CreateVideoGameRequestValidator : AbstractValidator<CreateVideoGameRequest>
    {
        public CreateVideoGameRequestValidator()
        {
            RuleFor(createVideoGameRequest => createVideoGameRequest.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createVideoGameRequest => createVideoGameRequest.DeveloperId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(createVideoGameRequest => createVideoGameRequest.ReleaseDate)
                .LessThanOrEqualTo(videoGameDto => videoGameDto.PurchaseDate)
                .WithMessage("{PropertyName} must be less than or equal to {ComparisonProperty}.");

            RuleFor(createVideoGameRequest => createVideoGameRequest.PurchaseDate)
                .GreaterThanOrEqualTo(videoGameDto => videoGameDto.ReleaseDate)
                .WithMessage("{PropertyName} must be greater than or equal to {ComparisonProperty}.");

            RuleFor(createVideoGameRequest => createVideoGameRequest.Price)
                .GreaterThanOrEqualTo(0.0M)
                .WithMessage("{PropertyName} must be greater than 0.0.");

            RuleFor(createVideoGameRequest => createVideoGameRequest.TotalTimePlayed)
                .GreaterThanOrEqualTo(TimeSpan.Zero)
                .WithMessage("{PropertyName} must be greater than or equal to " + $"{TimeSpan.Zero}.");
        }
    }
}
