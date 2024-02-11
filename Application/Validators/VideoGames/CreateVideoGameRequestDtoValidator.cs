using Application.Dtos.VideoGames;
using Application.Interfaces.Persistance;
using FluentValidation;

namespace Application.Validators.VideoGames
{
    public class CreateVideoGameRequestDtoValidator : AbstractValidator<CreateVideoGameRequestDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateVideoGameRequestDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

            RuleFor(createVideoGameRequestDto => createVideoGameRequestDto.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (title, cancellation) =>
                {
                    var videoGame = await _unitOfWork.VideoGameRepository.ReadByTitleAsync(title);
                    return videoGame.Id == 0;
                })
                .WithMessage("{PropertyName} must be unique.");

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
