using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;

namespace Application.Validators.Dtos
{
    public class ConsoleDtoValidator : AbstractValidator<ConsoleDto>
    {
        public ConsoleDtoValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(consoleDto => consoleDto.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (name, cancellationToken) =>
                {
                    var consoleDtos = await unitOfWork.ConsoleRepository.ReadByNameAsync(name, cancellationToken);
                    return consoleDtos.Any() == false;
                })
                .WithMessage("{PropertyName} must be unique.");

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
