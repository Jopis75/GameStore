using Application.Dtos.Common.Validators;
using FluentValidation;

namespace Application.Dtos.Products.Validators
{
    public class DeleteProductRequestDtoValidator : AbstractValidator<DeleteProductRequestDto>
    {
        public DeleteProductRequestDtoValidator()
        {
            Include(new DeleteRequestDtoValidator());
        }
    }
}
