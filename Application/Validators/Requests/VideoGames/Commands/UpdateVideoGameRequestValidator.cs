using Application.Features.VideoGames.Requests.Commands;
using Application.Interfaces.Persistance;
using Application.Validators.Dtos;
using FluentValidation;

namespace Application.Validators.Requests.VideoGames.Commands
{
    public class UpdateVideoGameRequestValidator : AbstractValidator<UpdateVideoGameRequest>
    {
        public UpdateVideoGameRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(updateVideoGameRequest => updateVideoGameRequest.VideoGameDto)
                .NotNull()
                .SetValidator(updateVideoGameRequest => new VideoGameDtoValidator(unitOfWork));
        }
    }
}
