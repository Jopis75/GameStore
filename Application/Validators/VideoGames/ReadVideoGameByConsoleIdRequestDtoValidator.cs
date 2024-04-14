using Application.Dtos.VideoGames;
using FluentValidation;

namespace Application.Validators.VideoGames
{
    public class ReadVideoGameByConsoleIdRequestDtoValidator : AbstractValidator<ReadVideoGameByConsoleIdRequestDto>
    {
        public ReadVideoGameByConsoleIdRequestDtoValidator()
        {
            RuleFor(readVideoGameByConsoleIdRequestDto => readVideoGameByConsoleIdRequestDto.ConsoleId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
