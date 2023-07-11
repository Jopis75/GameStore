using Application.Dtos.ConsoleProducts;
using Application.Validators.Common;
using FluentValidation;

namespace Application.Validators.ConsoleProducts
{
    public class ReadByIdConsoleProductRequestDtoValidator : AbstractValidator<ReadByIdConsoleProductRequestDto>
    {
        public ReadByIdConsoleProductRequestDtoValidator()
        {
            Include(new ReadByIdRequestDtoValidator());
        }
    }
}
