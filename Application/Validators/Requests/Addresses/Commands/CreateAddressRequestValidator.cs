using Application.Features.Addresses.Requests.Commands;
using Application.Interfaces.Persistance;
using Application.Validators.Dtos;
using FluentValidation;

namespace Application.Validators.Requests.Addresses.Commands
{
    public class CreateAddressRequestValidator : AbstractValidator<CreateAddressRequest>
    {
        public CreateAddressRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(createAddressRequest => createAddressRequest.AddressDto)
                .NotNull()
                .SetValidator(createAddressRequest => new AddressDtoValidator(unitOfWork));
        }
    }
}
