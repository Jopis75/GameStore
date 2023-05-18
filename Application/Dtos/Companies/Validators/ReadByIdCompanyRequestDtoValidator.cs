using Application.Dtos.Common.Validators;
using FluentValidation;

namespace Application.Dtos.Companies.Validators
{
    public class ReadByIdCompanyRequestDtoValidator : AbstractValidator<ReadByIdCompanyRequestDto>
    {
        public ReadByIdCompanyRequestDtoValidator()
        {
            Include(new ReadByIdRequestDtoValidator());
        }
    }
}
