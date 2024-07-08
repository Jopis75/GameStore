using Application.Features.VideoGames.Requests.Queries;
using FluentValidation;

namespace Application.Validators.Requests.VideoGames.Queries
{
    public class ReadMostPlayedVideoGameByConsoleIdRequestValidator : AbstractValidator<ReadMostPlayedVideoGameByConsoleIdRequest>
    {
        public ReadMostPlayedVideoGameByConsoleIdRequestValidator()
        {
            RuleFor(readMostPlayedVideoGameByConsoleIdRequest => readMostPlayedVideoGameByConsoleIdRequest.ConsoleId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
