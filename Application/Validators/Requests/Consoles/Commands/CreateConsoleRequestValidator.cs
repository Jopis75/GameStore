using Application.Features.Consoles.Requests.Commands;
using Application.Interfaces.Persistance;
using FluentValidation;

namespace Application.Validators.Requests.Consoles.Commands
{
    public class CreateConsoleRequestValidator : AbstractValidator<CreateConsoleRequest>
    {
        public CreateConsoleRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(createConsoleRequest => createConsoleRequest.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createConsoleRequest => createConsoleRequest.DeveloperId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(createConsoleRequest => createConsoleRequest.ReleaseDate)
                .LessThanOrEqualTo(consoleDto => consoleDto.PurchaseDate)
                .WithMessage("{PropertyName} must be less than or equal to {ComparisonProperty}.");

            RuleFor(createConsoleRequest => createConsoleRequest.PurchaseDate)
                .GreaterThanOrEqualTo(consoleDto => consoleDto.ReleaseDate)
                .WithMessage("{PropertyName} must be greater than or equal to {ComparisonProperty}.");

            RuleFor(createConsoleRequest => createConsoleRequest.Price)
                .GreaterThanOrEqualTo(0.0M)
                .WithMessage("{PropertyName} must be greater than or equal to 0.0.");
        }
    }
}
