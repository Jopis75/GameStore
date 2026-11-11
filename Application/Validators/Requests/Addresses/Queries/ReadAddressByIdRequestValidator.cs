using FluentValidation;
using GameStore.Application.Features.Addresses.Requests.Queries;

namespace GameStore.Application.Validators.Requests.Addresses.Queries
{
    public class ReadAddressByIdRequestValidator : AbstractValidator<ReadAddressByIdRequest>
    {
        public ReadAddressByIdRequestValidator()
        {
            RuleFor(readAddressByIdRequest => readAddressByIdRequest.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
