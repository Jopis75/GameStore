using Application.Features.Trophies.Requests.Commands;
using Application.Interfaces.Persistance;
using Application.Validators.Dtos;
using FluentValidation;

namespace Application.Validators.Requests.Trophies.Commands
{
    public class CreateTrophyRequestValidator : AbstractValidator<CreateTrophyRequest>
    {
        public CreateTrophyRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(createTrophyRequest => createTrophyRequest.TrophyDto)
                .NotNull()
                .SetValidator(createTrophyRequest => new TrophyDtoValidator(unitOfWork));
        }
    }
}
