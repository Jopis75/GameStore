using Application.Features.Addresses.Requests.Commands;
using Application.Interfaces.Persistance;
using Application.Validators.Dtos;
using FluentValidation;

namespace Application.Validators.Requests.Addresses.Commands
{
    public class UpdateAddressRequestValidator : AbstractValidator<UpdateAddressRequest>
    {
        public UpdateAddressRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(updateAddressRequest => updateAddressRequest.AddressDto)
                .NotNull()
                .SetValidator(updateAddressRequest => new AddressDtoValidator(unitOfWork));
        }
    }
}
