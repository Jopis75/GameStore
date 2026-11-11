using FluentValidation;
using GameStore.Application.Features.VideoGames.Requests.Commands;

namespace GameStore.Application.Validators.Requests.VideoGames.Commands
{
    public class DeleteVideoGameRequestValidator : AbstractValidator<DeleteVideoGameRequest>
    {
        public DeleteVideoGameRequestValidator()
        {
            RuleFor(deleteVideoGameRequest => deleteVideoGameRequest.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
