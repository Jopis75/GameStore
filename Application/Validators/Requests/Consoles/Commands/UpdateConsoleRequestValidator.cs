using Application.Features.Consoles.Requests.Commands;
using Application.Interfaces.Persistance;
using FluentValidation;

namespace Application.Validators.Requests.Consoles.Commands
{
    public class UpdateConsoleRequestValidator : AbstractValidator<UpdateConsoleRequest>
    {
        public UpdateConsoleRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(updateConsoleRequest => updateConsoleRequest.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(updateConsoleRequest => updateConsoleRequest.DeveloperId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(updateConsoleRequest => updateConsoleRequest.ReleaseDate)
                .LessThanOrEqualTo(consoleDto => consoleDto.PurchaseDate)
                .WithMessage("{PropertyName} must be less than or equal to {ComparisonProperty}.");

            RuleFor(updateConsoleRequest => updateConsoleRequest.PurchaseDate)
                .GreaterThanOrEqualTo(consoleDto => consoleDto.ReleaseDate)
                .WithMessage("{PropertyName} must be greater than or equal to {ComparisonProperty}.");

            RuleFor(updateConsoleRequest => updateConsoleRequest.Price)
                .GreaterThanOrEqualTo(0.0M)
                .WithMessage("{PropertyName} must be greater than or equal to 0.0.");
        }
    }
}
