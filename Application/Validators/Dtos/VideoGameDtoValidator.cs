using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;

namespace Application.Validators.Dtos
{
    public class VideoGameDtoValidator : AbstractValidator<VideoGameDto>
    {
        public VideoGameDtoValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(videoGameDto => videoGameDto.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (title, cancellationToken) =>
                {
                    var videoGameDtos = await unitOfWork.VideoGameRepository.ReadByTitleAsync(title, cancellationToken);
                    return videoGameDtos.Any() == false;
                })
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(videoGameDto => videoGameDto.DeveloperId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(videoGameDto => videoGameDto.ReleaseDate)
                .LessThanOrEqualTo(videoGameDto => videoGameDto.PurchaseDate)
                .WithMessage("{PropertyName} must be less than or equal to {ComparisonProperty}.");

            RuleFor(videoGameDto => videoGameDto.PurchaseDate)
                .GreaterThanOrEqualTo(videoGameDto => videoGameDto.ReleaseDate)
                .WithMessage("{PropertyName} must be greater than or equal to {ComparisonProperty}.");

            RuleFor(videoGameDto => videoGameDto.Price)
                .GreaterThanOrEqualTo(0.0M)
                .WithMessage("{PropertyName} must be greater than 0.0.");

            RuleFor(videoGameDto => videoGameDto.TotalTimePlayed)
                .GreaterThanOrEqualTo(TimeSpan.Zero)
                .WithMessage("{PropertyName} must be greater than or equal to " + $"{TimeSpan.Zero}.");
        }
    }
}
