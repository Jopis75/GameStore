using Application.Features.Genres.Requests.Commands;
using FluentValidation;

namespace Application.Validators.Requests.Genres.Commands
{
    public class UpdateGenreRequestValidator : AbstractValidator<UpdateGenreRequest>
    {
        public UpdateGenreRequestValidator()
        {
            RuleFor(updateGenreRequest => updateGenreRequest.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(updateGenreRequest => updateGenreRequest.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}
