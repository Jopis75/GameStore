using Application.Features.Genres.Requests.Commands;
using Application.Interfaces.Persistance;
using Application.Validators.Dtos;
using FluentValidation;

namespace Application.Validators.Requests.Genres.Commands
{
    public class UpdateGenreRequestValidator : AbstractValidator<UpdateGenreRequest>
    {
        public UpdateGenreRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(updateGenreRequest => updateGenreRequest.GenreDto)
                .NotNull()
                .SetValidator(updateGenreRequest => new GenreDtoValidator(unitOfWork));
        }
    }
}
