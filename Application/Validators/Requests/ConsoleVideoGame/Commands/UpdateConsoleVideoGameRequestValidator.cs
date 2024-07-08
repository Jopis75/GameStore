using Application.Features.ConsoleVideoGames.Requests.Commands;
using Application.Validators.Dtos;
using FluentValidation;

namespace Application.Validators.Requests.ConsoleVideoGame.Commands
{
    public class UpdateConsoleVideoGameRequestValidator : AbstractValidator<UpdateConsoleVideoGameRequest>
    {
        public UpdateConsoleVideoGameRequestValidator()
        {
            RuleFor(updateConsoleVideoGameRequest => updateConsoleVideoGameRequest.ConsoleVideoGameDto)
                .NotNull()
                .SetValidator(updateConsoleVideoGameRequest => new ConsoleVideoGameDtoValidator());
        }
    }
}
