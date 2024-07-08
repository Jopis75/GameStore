using Application.Features.Consoles.Requests.Commands;
using Application.Interfaces.Persistance;
using Application.Validators.Dtos;
using FluentValidation;

namespace Application.Validators.Requests.Consoles.Commands
{
    public class CreateConsoleRequestValidator : AbstractValidator<CreateConsoleRequest>
    {
        public CreateConsoleRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(createConsoleRequest => createConsoleRequest.ConsoleDto)
                .NotNull()
                .SetValidator(createConsoleRequest => new ConsoleDtoValidator(unitOfWork));
        }
    }
}
