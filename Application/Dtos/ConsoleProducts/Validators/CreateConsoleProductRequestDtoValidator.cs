using FluentValidation;

namespace Application.Dtos.ConsoleProducts.Validators
{
    public class CreateConsoleProductRequestDtoValidator : AbstractValidator<CreateConsoleProductRequestDto>
    {
        public CreateConsoleProductRequestDtoValidator()
        {
            RuleFor(createConsoleProductRequestDto => createConsoleProductRequestDto.ConsoleId)
                .NotEqual(0)
                .WithMessage("{PropertyName} must not equal 0.");

            RuleFor(createConsoleProductRequestDto => createConsoleProductRequestDto.ProductId)
                .NotEqual(0)
                .WithMessage("{PropertyName} must not equal 0.");
        }
    }
}
