using Application.Dtos.Common.Validators;
using FluentValidation;

namespace Application.Dtos.Products.Validators
{
    public class ReadByIdProductRequestDtoValidator : AbstractValidator<ReadByIdProductRequestDto>
    {
        public ReadByIdProductRequestDtoValidator()
        {
            Include(new ReadByIdRequestDtoValidator());
        }
    }
}
