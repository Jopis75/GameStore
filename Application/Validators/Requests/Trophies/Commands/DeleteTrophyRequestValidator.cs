using Application.Features.Trophies.Requests.Commands;
using FluentValidation;

namespace Application.Validators.Requests.Trophies.Commands
{
    public class DeleteTrophyRequestValidator : AbstractValidator<DeleteTrophyRequest>
    {
        public DeleteTrophyRequestValidator()
        {
            RuleFor(deleteTrophyRequest => deleteTrophyRequest.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
