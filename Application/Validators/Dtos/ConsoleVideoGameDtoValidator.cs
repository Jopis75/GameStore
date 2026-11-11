using FluentValidation;
using GameStore.Domain.Dtos;

namespace GameStore.Application.Validators.Dtos
{
    public class ConsoleVideoGameDtoValidator : AbstractValidator<ConsoleVideoGameDto>
    {
        public ConsoleVideoGameDtoValidator()
        {
            RuleFor(cnsoleVideoGameDto => cnsoleVideoGameDto.ConsoleId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(consoleVideoGameDto => consoleVideoGameDto.VideoGameId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
