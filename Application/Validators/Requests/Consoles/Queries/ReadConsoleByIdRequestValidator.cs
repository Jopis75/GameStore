using FluentValidation;
using GameStore.Application.Features.Consoles.Requests.Queries;

namespace GameStore.Application.Validators.Requests.Consoles.Queries
{
    public class ReadConsoleByIdRequestValidator : AbstractValidator<ReadConsoleByIdRequest>
    {
        public ReadConsoleByIdRequestValidator()
        {
            RuleFor(readConsoleByIdRequest => readConsoleByIdRequest.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
