using Application.Features.Trophies.Requests.Queries;
using FluentValidation;

namespace Application.Validators.Requests.Trophies.Queries
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
