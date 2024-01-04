using Application.Dtos.Addresses;
using Application.Validators.Common;
using FluentValidation;

namespace Application.Validators.Addresses
{
    public class ReadByIdAddressRequestDtoValidator : AbstractValidator<ReadAddressByIdRequestDto>
    {
        public ReadByIdAddressRequestDtoValidator()
        {
            Include(new ReadByIdRequestDtoValidator());
        }
    }
}
