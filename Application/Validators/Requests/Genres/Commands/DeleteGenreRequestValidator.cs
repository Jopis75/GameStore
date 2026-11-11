using FluentValidation;
using GameStore.Application.Features.Genres.Requests.Commands;

namespace GameStore.Application.Validators.Requests.Genres.Commands
{
    public class DeleteGenreRequestValidator : AbstractValidator<DeleteGenreRequest>
    {
        public DeleteGenreRequestValidator()
        {
            RuleFor(deleteGenreRequest => deleteGenreRequest.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
