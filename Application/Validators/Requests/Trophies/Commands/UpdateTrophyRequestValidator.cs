using Application.Features.Trophies.Requests.Commands;
using Application.Interfaces.Persistance;
using FluentValidation;

namespace Application.Validators.Requests.Trophies.Commands
{
    public class UpdateTrophyRequestValidator : AbstractValidator<UpdateTrophyRequest>
    {
        public UpdateTrophyRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(updateTrophyRequest => updateTrophyRequest.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(updateTrophyRequest => updateTrophyRequest.Description)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(updateTrophyRequest => updateTrophyRequest.TrophyValue)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(updateTrophyRequest => updateTrophyRequest.VideoGameId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
