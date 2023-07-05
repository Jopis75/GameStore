using Application.Dtos.Common.Validators;
using FluentValidation;

namespace Application.Dtos.Consoles.Validators
{
    public class DeleteConsoleRequestDtoValidator : AbstractValidator<DeleteConsoleRequestDto>
    {
        public DeleteConsoleRequestDtoValidator()
        {
            Include(new DeleteRequestDtoValidator());
        }
    }
}
