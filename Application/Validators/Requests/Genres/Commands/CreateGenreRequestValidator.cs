using FluentValidation;
using GameStore.Application.Features.Genres.Requests.Commands;

namespace GameStore.Application.Validators.Requests.Genres.Commands
{
    public class CreateGenreRequestValidator : AbstractValidator<CreateGenreRequest>
    {
        public CreateGenreRequestValidator()
        {
            RuleFor(createGenreRequest => createGenreRequest.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}
