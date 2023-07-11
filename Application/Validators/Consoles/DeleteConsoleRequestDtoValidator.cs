using Application.Dtos.Consoles;
using Application.Validators.Common;
using FluentValidation;

namespace Application.Validators.Consoles
{
    public class DeleteConsoleRequestDtoValidator : AbstractValidator<DeleteConsoleRequestDto>
    {
        public DeleteConsoleRequestDtoValidator()
        {
            Include(new DeleteRequestDtoValidator());
        }
    }
}
