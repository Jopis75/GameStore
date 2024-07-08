using Application.Features.VideoGames.Requests.Queries;
using FluentValidation;

namespace Application.Validators.Requests.VideoGames.Queries
{
    public class ReadVideoGameByIdRequestValidator : AbstractValidator<ReadVideoGameByIdRequest>
    {
        public ReadVideoGameByIdRequestValidator()
        {
            RuleFor(readVideoGameByIdRequest => readVideoGameByIdRequest.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
