using Application.Features.Consoles.Requests.Queries;
using FluentValidation;

namespace Application.Validators.Requests.Consoles.Queries
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
