using Application.Dtos.Products;
using Application.Validators.Common;
using FluentValidation;

namespace Application.Validators.Products
{
    public class DeleteProductRequestDtoValidator : AbstractValidator<DeleteProductRequestDto>
    {
        public DeleteProductRequestDtoValidator()
        {
            Include(new DeleteRequestDtoValidator());
        }
    }
}
