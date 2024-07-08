using Application.Features.VideoGames.Requests.Commands;
using Application.Interfaces.Persistance;
using Application.Validators.Dtos;
using FluentValidation;

namespace Application.Validators.Requests.VideoGames.Commands
{
    public class CreateVideoGameRequestValidator : AbstractValidator<CreateVideoGameRequest>
    {
        public CreateVideoGameRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(createVideoGameRequest => createVideoGameRequest.VideoGameDto)
                .NotNull()
                .SetValidator(createVideoGameRequest => new VideoGameDtoValidator(unitOfWork));
        }
    }
}
