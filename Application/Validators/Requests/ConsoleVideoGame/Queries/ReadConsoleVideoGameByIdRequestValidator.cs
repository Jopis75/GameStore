using Application.Features.ConsoleVideoGames.Requests.Queries;
using FluentValidation;

namespace Application.Validators.Requests.ConsoleVideoGame.Queries
{
    public class ReadConsoleVideoGameByIdRequestValidator : AbstractValidator<ReadConsoleVideoGameByIdRequest>
    {
        public ReadConsoleVideoGameByIdRequestValidator()
        {
            RuleFor(readConsoleVideoGameByIdRequest => readConsoleVideoGameByIdRequest.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
