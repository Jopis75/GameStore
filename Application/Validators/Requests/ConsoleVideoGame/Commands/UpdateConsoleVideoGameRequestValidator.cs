using FluentValidation;
using GameStore.Application.Features.ConsoleVideoGames.Requests.Commands;

namespace GameStore.Application.Validators.Requests.ConsoleVideoGame.Commands
{
    public class UpdateConsoleVideoGameRequestValidator : AbstractValidator<UpdateConsoleVideoGameRequest>
    {
        public UpdateConsoleVideoGameRequestValidator()
        {
            RuleFor(updateConsoleVideoGameRequest => updateConsoleVideoGameRequest.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(updateConsoleVideoGameRequest => updateConsoleVideoGameRequest.ConsoleId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(updateConsoleVideoGameRequest => updateConsoleVideoGameRequest.VideoGameId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
