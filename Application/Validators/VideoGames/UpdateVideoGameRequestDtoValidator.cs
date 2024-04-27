using Application.Dtos.VideoGames;
using Application.Interfaces.Persistance;
using Application.Validators.Common;
using FluentValidation;

namespace Application.Validators.VideoGames
{
    public class UpdateVideoGameRequestDtoValidator : AbstractValidator<UpdateVideoGameRequestDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateVideoGameRequestDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

            Include(new UpdateRequestDtoValidator());

            RuleFor(updateVideoGameRequestDto => updateVideoGameRequestDto.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (title, cancellation) =>
                {
                    var videoGames = await _unitOfWork.VideoGameRepository.ReadByTitleAsync(title);
                    return videoGames.Count() == 0;
                })
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(updateVideoGameRequestDto => updateVideoGameRequestDto.DeveloperId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

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
