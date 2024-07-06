using Application.Features.Addresses.Requests.Queries;
using FluentValidation;

namespace Application.Validators.Requests.Addresses.Queries
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
