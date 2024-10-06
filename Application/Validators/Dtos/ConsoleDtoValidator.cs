using Domain.Dtos;
using FluentValidation;

namespace Application.Validators.Dtos
{
    public class ConsoleDtoValidator : AbstractValidator<ConsoleDto>
    {
        public ConsoleDtoValidator()
        {
            RuleFor(consoleDto => consoleDto.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(consoleDto => consoleDto.DeveloperId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(consoleDto => consoleDto.ReleaseDate)
                .LessThanOrEqualTo(consoleDto => consoleDto.PurchaseDate)
                .WithMessage("{PropertyName} must be less than or equal to {ComparisonProperty}.");

            RuleFor(consoleDto => consoleDto.PurchaseDate)
                .GreaterThanOrEqualTo(consoleDto => consoleDto.ReleaseDate)
                .WithMessage("{PropertyName} must be greater than or equal to {ComparisonProperty}.");

            RuleFor(consoleDto => consoleDto.Price)
                .GreaterThanOrEqualTo(0.0M)
                .WithMessage("{PropertyName} must be greater than or equal to 0.0.");
        }
    }
}
