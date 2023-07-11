using Application.Dtos.ConsoleProducts;
using Application.Validators.Common;
using FluentValidation;

namespace Application.Validators.ConsoleProducts
{
    public class DeleteConsoleProductRequestDtoValidator : AbstractValidator<DeleteConsoleProductRequestDto>
    {
        public DeleteConsoleProductRequestDtoValidator()
        {
            Include(new DeleteRequestDtoValidator());
        }
    }
}
