using Application.Dtos.Addresses;
using Application.Validators.Common;
using FluentValidation;

namespace Application.Validators.Addresses
{
    public class DeleteAddressRequestDtoValidator : AbstractValidator<DeleteAddressRequestDto>
    {
        public DeleteAddressRequestDtoValidator()
        {
            Include(new DeleteRequestDtoValidator());
        }
    }
}
