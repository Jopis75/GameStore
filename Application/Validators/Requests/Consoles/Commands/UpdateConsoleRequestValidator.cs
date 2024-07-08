using Application.Features.Consoles.Requests.Commands;
using Application.Interfaces.Persistance;
using Application.Validators.Dtos;
using FluentValidation;

namespace Application.Validators.Requests.Consoles.Commands
{
    public class UpdateConsoleRequestValidator : AbstractValidator<UpdateConsoleRequest>
    {
        public UpdateConsoleRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(updateConsoleRequest => updateConsoleRequest.ConsoleDto)
                .NotNull()
                .SetValidator(updateConsoleRequest => new ConsoleDtoValidator(unitOfWork));
        }
    }
}
