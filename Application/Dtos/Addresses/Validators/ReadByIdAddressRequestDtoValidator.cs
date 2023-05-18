using Application.Dtos.Common.Validators;
using FluentValidation;

namespace Application.Dtos.Addresses.Validators
{
    public class ReadByIdAddressRequestDtoValidator : AbstractValidator<ReadByIdAddressRequestDto>
    {
        public ReadByIdAddressRequestDtoValidator()
        {
            Include(new ReadByIdRequestDtoValidator());
        }
    }
}
