using Application.Dtos.VideoGames;
using FluentValidation;

namespace Application.Validators.VideoGames
{
    public class ReadMostPlayedVideoGameByConsoleIdRequestDtoValidator : AbstractValidator<ReadMostPlayedVideoGameByConsoleIdRequestDto>
    {
        public ReadMostPlayedVideoGameByConsoleIdRequestDtoValidator()
        {
            RuleFor(readMostPlayedVideoGameByConsoleIdRequestDto => readMostPlayedVideoGameByConsoleIdRequestDto.ConsoleId)
                .NotEqual(0)
                .WithMessage("{PropertyName} must not equal 0.");
        }
    }
}
