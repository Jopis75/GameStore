using FluentValidation;
using GameStore.Application.Features.VideoGames.Requests.Queries;

namespace GameStore.Application.Validators.Requests.VideoGames.Queries
{
    public class ReadVideoGamesByConsoleIdRequestValidator : AbstractValidator<ReadVideoGamesByConsoleIdRequest>
    {
        public ReadVideoGamesByConsoleIdRequestValidator()
        {
            RuleFor(readVideoGamesByConsoleIdRequest => readVideoGamesByConsoleIdRequest.ConsoleId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
