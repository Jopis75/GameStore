using Application.Dtos.Addresses;
using Application.Validators.Common;
using FluentValidation;

namespace Application.Validators.Addresses
{
    public class ReadByIdAddressRequestDtoValidator : AbstractValidator<ReadByIdAddressRequestDto>
    {
        public ReadByIdAddressRequestDtoValidator()
        {
            Include(new ReadByIdRequestDtoValidator());
        }
    }
}
