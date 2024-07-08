using Application.Features.ConsoleVideoGames.Requests.Commands;
using Application.Validators.Dtos;
using FluentValidation;

namespace Application.Validators.Requests.ConsoleVideoGame.Commands
{
    public class CreateConsoleVideoGameRequestValidator : AbstractValidator<CreateConsoleVideoGameRequest>
    {
        public CreateConsoleVideoGameRequestValidator()
        {
            RuleFor(createConsoleVideoGameRequest => createConsoleVideoGameRequest.ConsoleVideoGameDto)
                .NotNull()
                .SetValidator(createConsoleVideoGameRequest => new ConsoleVideoGameDtoValidator());
        }
    }
}
