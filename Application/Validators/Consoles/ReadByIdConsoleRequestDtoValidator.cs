using Application.Dtos.Consoles;
using Application.Validators.Common;
using FluentValidation;

namespace Application.Validators.Consoles
{
    public class ReadByIdConsoleRequestDtoValidator : AbstractValidator<ReadConsoleByIdRequestDto>
    {
        public ReadByIdConsoleRequestDtoValidator()
        {
            Include(new ReadByIdRequestDtoValidator());
        }
    }
}
