using Application.Dtos.Products;
using Application.Validators.Common;
using FluentValidation;

namespace Application.Validators.Products
{
    public class ReadByIdProductRequestDtoValidator : AbstractValidator<ReadByIdProductRequestDto>
    {
        public ReadByIdProductRequestDtoValidator()
        {
            Include(new ReadByIdRequestDtoValidator());
        }
    }
}
