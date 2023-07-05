using Application.Dtos.Common.Validators;
using FluentValidation;

namespace Application.Dtos.ConsoleProducts.Validators
{
    public class ReadByIdConsoleProductRequestDtoValidator : AbstractValidator<ReadByIdConsoleProductRequestDto>
    {
        public ReadByIdConsoleProductRequestDtoValidator()
        {
            Include(new ReadByIdRequestDtoValidator());
        }
    }
}
