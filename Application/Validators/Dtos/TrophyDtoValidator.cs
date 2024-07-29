using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;

namespace Application.Validators.Dtos
{
    public class TrophyDtoValidator : AbstractValidator<TrophyDto>
    {
        public TrophyDtoValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(trophyDto => trophyDto.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (name, cancellationToken) =>
                {
                    var trophyDtos = await unitOfWork.TrophyRepository.ReadByNameAsync(name, cancellationToken);
                    return trophyDtos.Any() == false;
                })
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(trophyDto => trophyDto.Description)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(trophyDto => trophyDto.TrophyValue)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(trophyDto => trophyDto.VideoGameId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
