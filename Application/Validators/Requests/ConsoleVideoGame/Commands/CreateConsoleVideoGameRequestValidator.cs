using Application.Features.ConsoleVideoGames.Requests.Commands;
using FluentValidation;

namespace Application.Validators.Requests.ConsoleVideoGame.Commands
{
    public class CreateConsoleVideoGameRequestValidator : AbstractValidator<CreateConsoleVideoGameRequest>
    {
        public CreateConsoleVideoGameRequestValidator()
        {
            RuleFor(createConsoleVideoGameRequest => createConsoleVideoGameRequest.ConsoleId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(createConsoleVideoGameRequest => createConsoleVideoGameRequest.VideoGameId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
