using Application.Dtos.Common.Validators;
using FluentValidation;

namespace Application.Dtos.Addresses.Validators
{
    public class DeleteAddressRequestDtoValidator : AbstractValidator<DeleteAddressRequestDto>
    {
        public DeleteAddressRequestDtoValidator()
        {
            Include(new DeleteRequestDtoValidator());
        }
    }
}
