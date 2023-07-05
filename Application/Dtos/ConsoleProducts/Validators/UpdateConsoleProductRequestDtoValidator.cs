using Application.Dtos.Common.Validators;
using FluentValidation;

namespace Application.Dtos.ConsoleProducts.Validators
{
    public class UpdateConsoleProductRequestDtoValidator : AbstractValidator<UpdateConsoleProductRequestDto>
    {
        public UpdateConsoleProductRequestDtoValidator()
        {
            Include(new UpdateRequestDtoValidator());

            RuleFor(createConsoleProductRequestDto => createConsoleProductRequestDto.ConsoleId)
                .NotEqual(0)
                .WithMessage("{PropertyName} must not equal 0.");

            RuleFor(createConsoleProductRequestDto => createConsoleProductRequestDto.ProductId)
                .NotEqual(0)
                .WithMessage("{PropertyName} must not equal 0.");
        }
    }
}
