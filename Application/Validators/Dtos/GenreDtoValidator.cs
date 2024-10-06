using Domain.Dtos;
using FluentValidation;

namespace Application.Validators.Dtos
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
