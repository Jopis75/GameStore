using Application.Features.VideoGames.Requests.Commands;
using FluentValidation;

namespace Application.Validators.Requests.VideoGames.Commands
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
