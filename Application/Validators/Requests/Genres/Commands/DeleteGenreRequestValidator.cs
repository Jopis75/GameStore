using Application.Features.Genres.Requests.Commands;
using FluentValidation;

namespace Application.Validators.Requests.Genres.Commands
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
