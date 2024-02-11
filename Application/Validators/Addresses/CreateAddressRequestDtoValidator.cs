using Application.Dtos.Addresses;
using Application.Interfaces.Persistance;
using FluentValidation;

namespace Application.Validators.Addresses
{
    //LINE 1: ADDRESSEE'S FULL LEGAL NAME
    //LINE 2: STREET ADDRESS OR POST OFFICE BOX NUMBER
    //LINE 3: CITY OR TOWN NAME, OTHER PRINCIPAL SUBDIVISION(i.e.PROVINCE, STATE, COUNTY, ETC.) AND POSTAL CODE(IF KNOWN) (Note: in some countries, the postal code may precede the city or town name)
    //LINE 4: COUNTRY NAME(UPPERCASE LETTERS IN ENGLISH)
    public class CreateAddressRequestDtoValidator : AbstractValidator<CreateAddressRequestDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAddressRequestDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

            RuleFor(createAddressRequestDto => createAddressRequestDto.StreetAddress)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (streetAddress, cancellation) =>
                {
                    var address = await _unitOfWork.AddressRepository.ReadByStreetAddressAsync(streetAddress);
                    return address.Id == 0;
                })
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(createAddressRequestDto => createAddressRequestDto.City)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createAddressRequestDto => createAddressRequestDto.State)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createAddressRequestDto => createAddressRequestDto.PostalCode)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createAddressRequestDto => createAddressRequestDto.Country)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}
