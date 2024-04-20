using Application.Dtos.Addresses;
using Application.Interfaces.Persistance;
using Application.Validators.Common;
using FluentValidation;

namespace Application.Validators.Addresses
{
    public class UpdateAddressRequestDtoValidator : AbstractValidator<UpdateAddressRequestDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAddressRequestDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

            Include(new UpdateRequestDtoValidator());

            RuleFor(updateAddressRequestDto => updateAddressRequestDto.StreetAddress)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (streetAddress, cancellation) =>
                {
                    var addresses = await _unitOfWork.AddressRepository.ReadByStreetAddressAsync(streetAddress);
                    return addresses.Count() == 0;
                })
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(updateAddressRequestDto => updateAddressRequestDto.City)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(updateAddressRequestDto => updateAddressRequestDto.State)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(updateAddressRequestDto => updateAddressRequestDto.PostalCode)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(updateAddressRequestDto => updateAddressRequestDto.Country)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}
