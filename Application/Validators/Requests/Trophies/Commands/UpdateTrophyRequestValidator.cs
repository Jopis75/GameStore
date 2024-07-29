using Application.Features.Trophies.Requests.Commands;
using Application.Interfaces.Persistance;
using Application.Validators.Dtos;
using FluentValidation;

namespace Application.Validators.Requests.Trophies.Commands
{
    public class UpdateTrophyRequestValidator : AbstractValidator<UpdateTrophyRequest>
    {
        public UpdateTrophyRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(updateTrophyRequest => updateTrophyRequest.TrophyDto)
                .NotNull()
                .SetValidator(updateTrophyRequest => new TrophyDtoValidator(unitOfWork));
        }
    }
}
