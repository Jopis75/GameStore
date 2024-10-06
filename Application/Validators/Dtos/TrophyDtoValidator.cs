using Domain.Dtos;
using FluentValidation;

namespace Application.Validators.Dtos
{
    public class TrophyDtoValidator : AbstractValidator<TrophyDto>
    {
        public TrophyDtoValidator()
        {
            RuleFor(trophyDto => trophyDto.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

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
