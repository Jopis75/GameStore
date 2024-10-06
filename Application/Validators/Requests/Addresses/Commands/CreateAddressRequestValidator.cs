using Application.Features.Addresses.Requests.Commands;
using FluentValidation;

namespace Application.Validators.Requests.Addresses.Commands
{
    public class CreateAddressRequestValidator : AbstractValidator<CreateAddressRequest>
    {
        public CreateAddressRequestValidator()
        {
            RuleFor(createAddressRequest => createAddressRequest.StreetAddress)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createAddressRequest => createAddressRequest.City)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createAddressRequest => createAddressRequest.PostalCode)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}
