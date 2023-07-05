using Application.Dtos.Common.Validators;
using FluentValidation;

namespace Application.Dtos.Consoles.Validators
{
    public class ReadByIdConsoleRequestDtoValidator : AbstractValidator<ReadByIdConsoleRequestDto>
    {
        public ReadByIdConsoleRequestDtoValidator()
        {
            Include(new ReadByIdRequestDtoValidator());
        }
    }
}
