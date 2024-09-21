using Application.Features.Genres.Requests.Commands;
using Application.Interfaces.Persistance;
using FluentValidation;

namespace Application.Validators.Requests.Genres.Commands
{
    public class CreateGenreRequestValidator : AbstractValidator<CreateGenreRequest>
    {
        public CreateGenreRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(createGenreRequest => createGenreRequest.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}
