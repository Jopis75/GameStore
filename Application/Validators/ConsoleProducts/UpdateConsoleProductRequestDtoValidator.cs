using Application.Dtos.ConsoleProducts;
using Application.Validators.Common;
using FluentValidation;

namespace Application.Validators.ConsoleProducts
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
