using Application.Dtos.Common.Validators;
using FluentValidation;

namespace Application.Dtos.ConsoleProducts.Validators
{
    public class DeleteConsoleProductRequestDtoValidator : AbstractValidator<DeleteConsoleProductRequestDto>
    {
        public DeleteConsoleProductRequestDtoValidator()
        {
            Include(new DeleteRequestDtoValidator());
        }
    }
}
