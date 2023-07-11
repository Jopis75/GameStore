using Application.Dtos.Addresses;
using FluentValidation;

namespace Application.Validators.Addresses
{
    public class CreateAddressRequestDtoValidator : AbstractValidator<CreateAddressRequestDto>
    {
        //LINE 1: ADDRESSEE'S FULL LEGAL NAME
        //LINE 2: STREET ADDRESS OR POST OFFICE BOX NUMBER
        //LINE 3: CITY OR TOWN NAME, OTHER PRINCIPAL SUBDIVISION(i.e.PROVINCE, STATE, COUNTY, ETC.) AND POSTAL CODE(IF KNOWN) (Note: in some countries, the postal code may precede the city or town name)
        //LINE 4: COUNTRY NAME(UPPERCASE LETTERS IN ENGLISH)
        public CreateAddressRequestDtoValidator()
        {
            RuleFor(createAddressRequestDto => createAddressRequestDto.StreetAddress)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

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
