using FluentValidation;
using GameStore.Application.Features.Consoles.Requests.Commands;

namespace GameStore.Application.Validators.Requests.Consoles.Commands
{
    public class DeleteConsoleRequestValidator : AbstractValidator<DeleteConsoleRequest>
    {
        public DeleteConsoleRequestValidator()
        {
            RuleFor(deleteConsoleRequest => deleteConsoleRequest.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
