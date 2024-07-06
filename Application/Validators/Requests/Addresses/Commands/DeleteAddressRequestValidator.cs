using Application.Features.Addresses.Requests.Commands;
using FluentValidation;

namespace Application.Validators.Requests.Addresses.Commands
{
    public class DeleteAddressRequestValidator : AbstractValidator<DeleteAddressRequest>
    {
        public DeleteAddressRequestValidator()
        {
            RuleFor(deleteAddressRequest => deleteAddressRequest.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
