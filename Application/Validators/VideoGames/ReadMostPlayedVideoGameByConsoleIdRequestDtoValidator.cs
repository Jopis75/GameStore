using Application.Dtos.VideoGames;
using FluentValidation;

namespace Application.Validators.VideoGames
{
    public class ReadMostPlayedVideoGameByConsoleIdRequestDtoValidator : AbstractValidator<ReadMostPlayedVideoGameByConsoleIdRequestDto>
    {
        public ReadMostPlayedVideoGameByConsoleIdRequestDtoValidator()
        {
            RuleFor(readMostPlayedVideoGameByConsoleIdRequestDto => readMostPlayedVideoGameByConsoleIdRequestDto.ConsoleId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
