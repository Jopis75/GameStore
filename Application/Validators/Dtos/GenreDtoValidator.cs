using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;

namespace Application.Validators.Dtos
{
    public class GenreDtoValidator : AbstractValidator<GenreDto>
    {
        public GenreDtoValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(genreDto => genreDto.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (name, cancellationToken) =>
                {
                    var genreDtos = await unitOfWork.GenreRepository.ReadByNameAsync(name, cancellationToken);
                    return genreDtos.Any() == false;
                })
                .WithMessage("{PropertyName} must be unique.");
        }
    }
}
