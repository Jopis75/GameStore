using FluentValidation;
using GameStore.Domain.Dtos;

namespace GameStore.Application.Validators.Dtos
{
    public class GenreDtoValidator : AbstractValidator<GenreDto>
    {
        public GenreDtoValidator()
        {
            RuleFor(genreDto => genreDto.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}
