using Application.Features.Addresses.Requests.Commands;
using FluentValidation;

namespace Application.Validators.Requests.Addresses.Commands
{
    public class UpdateAddressRequestValidator : AbstractValidator<UpdateAddressRequest>
    {
        public UpdateAddressRequestValidator()
        {
            RuleFor(updateAddressRequest => updateAddressRequest.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(updateAddressRequest => updateAddressRequest.StreetAddress)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(updateAddressRequest => updateAddressRequest.City)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(updateAddressRequest => updateAddressRequest.PostalCode)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}
