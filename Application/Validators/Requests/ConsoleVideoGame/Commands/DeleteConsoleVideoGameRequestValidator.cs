using FluentValidation;
using GameStore.Application.Features.ConsoleVideoGames.Requests.Commands;

namespace GameStore.Application.Validators.Requests.ConsoleVideoGame.Commands
{
    public class DeleteConsoleVideoGameRequestValidator : AbstractValidator<DeleteConsoleVideoGameRequest>
    {
        public DeleteConsoleVideoGameRequestValidator()
        {
            RuleFor(deleteConsoleVideoGameRequest => deleteConsoleVideoGameRequest.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
