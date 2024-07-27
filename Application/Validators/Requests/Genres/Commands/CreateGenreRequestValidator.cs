using Application.Features.Genres.Requests.Commands;
using Application.Interfaces.Persistance;
using Application.Validators.Dtos;
using FluentValidation;

namespace Application.Validators.Requests.Genres.Commands
{
    public class CreateGenreRequestValidator : AbstractValidator<CreateGenreRequest>
    {
        public CreateGenreRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(createGenreRequest => createGenreRequest.GenreDto)
                .NotNull()
                .SetValidator(createGenreRequest => new GenreDtoValidator(unitOfWork));
        }
    }
}
