using FluentValidation;

namespace Application.Dtos.Consoles.Validators
{
    public class CreateConsoleRequestDtoValidator : AbstractValidator<CreateConsoleRequestDto>
    {
        public CreateConsoleRequestDtoValidator()
        {
            RuleFor(createConsoleRequestDto => createConsoleRequestDto.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createConsoleRequestDto => createConsoleRequestDto.DeveloperId)
                .NotEqual(0)
                .WithMessage("{PropertyName} must not equal 0.");

            RuleFor(createConsoleRequestDto => createConsoleRequestDto.ReleaseDate)
                .LessThanOrEqualTo(createConsoleRequestDto => createConsoleRequestDto.PurchaseDate)
                .WithMessage("{PropertyName} must be less than or equal to {ComparisonProperty}.");

            RuleFor(createConsoleRequestDto => createConsoleRequestDto.PurchaseDate)
                .GreaterThanOrEqualTo(createConsoleRequestDto => createConsoleRequestDto.ReleaseDate)
                .WithMessage("{PropertyName} must be greater than or equal to {ComparisonProperty}.");

            RuleFor(createConsoleRequestDto => createConsoleRequestDto.Price)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
