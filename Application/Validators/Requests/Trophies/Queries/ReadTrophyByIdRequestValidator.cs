using FluentValidation;
using GameStore.Application.Features.Trophies.Requests.Queries;

namespace GameStore.Application.Validators.Requests.Trophies.Queries
{
    public class ReadTrophyByIdRequestValidator : AbstractValidator<ReadTrophyByIdRequest>
    {
        public ReadTrophyByIdRequestValidator()
        {
            RuleFor(readTrophyByIdRequest => readTrophyByIdRequest.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
