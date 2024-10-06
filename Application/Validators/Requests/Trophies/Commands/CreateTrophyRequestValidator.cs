using Application.Features.Trophies.Requests.Commands;
using FluentValidation;

namespace Application.Validators.Requests.Trophies.Commands
{
    public class CreateTrophyRequestValidator : AbstractValidator<CreateTrophyRequest>
    {
        public CreateTrophyRequestValidator()
        {
            RuleFor(createTrophyRequest => createTrophyRequest.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createTrophyRequest => createTrophyRequest.Description)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createTrophyRequest => createTrophyRequest.TrophyValue)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createTrophyRequest => createTrophyRequest.VideoGameId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
