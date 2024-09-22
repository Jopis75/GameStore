using Application.Features.VideoGames.Requests.Commands;
using Application.Interfaces.Persistance;
using FluentValidation;

namespace Application.Validators.Requests.VideoGames.Commands
{
    public class UpdateVideoGameRequestValidator : AbstractValidator<UpdateVideoGameRequest>
    {
        public UpdateVideoGameRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(updateVideoGameRequest => updateVideoGameRequest.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(updateVideoGameRequest => updateVideoGameRequest.DeveloperId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(updateVideoGameRequest => updateVideoGameRequest.ReleaseDate)
                .LessThanOrEqualTo(videoGameDto => videoGameDto.PurchaseDate)
                .WithMessage("{PropertyName} must be less than or equal to {ComparisonProperty}.");

            RuleFor(updateVideoGameRequest => updateVideoGameRequest.PurchaseDate)
                .GreaterThanOrEqualTo(videoGameDto => videoGameDto.ReleaseDate)
                .WithMessage("{PropertyName} must be greater than or equal to {ComparisonProperty}.");

            RuleFor(updateVideoGameRequest => updateVideoGameRequest.Price)
                .GreaterThanOrEqualTo(0.0M)
                .WithMessage("{PropertyName} must be greater than 0.0.");

            RuleFor(updateVideoGameRequest => updateVideoGameRequest.TotalTimePlayed)
                .GreaterThanOrEqualTo(TimeSpan.Zero)
                .WithMessage("{PropertyName} must be greater than or equal to " + $"{TimeSpan.Zero}.");
        }
    }
}
