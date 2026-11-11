using FluentValidation;
using GameStore.Application.Features.ConsoleVideoGames.Requests.Queries;

namespace GameStore.Application.Validators.Requests.ConsoleVideoGame.Queries
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
