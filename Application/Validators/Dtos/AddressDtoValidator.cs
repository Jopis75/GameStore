using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;

namespace Application.Validators.Dtos
{
    //LINE 1: ADDRESSEE'S FULL LEGAL NAME
    //LINE 2: STREET ADDRESS OR POST OFFICE BOX NUMBER
    //LINE 3: CITY OR TOWN NAME, OTHER PRINCIPAL SUBDIVISION(i.e.PROVINCE, STATE, COUNTY, ETC.) AND POSTAL CODE(IF KNOWN) (Note: in some countries, the postal code may precede the city or town name)
    //LINE 4: COUNTRY NAME(UPPERCASE LETTERS IN ENGLISH)
    public class AddressDtoValidator : AbstractValidator<AddressDto>
    {
        public AddressDtoValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(addressDto => addressDto.StreetAddress)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(addressDto => addressDto.City)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(addressDto => addressDto.State)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(addressDto => addressDto.PostalCode)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(addressDto => addressDto.Country)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}
