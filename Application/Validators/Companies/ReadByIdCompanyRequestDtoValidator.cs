using Application.Dtos.Companies;
using Application.Validators.Common;
using FluentValidation;

namespace Application.Validators.Companies
{
    public class ReadByIdCompanyRequestDtoValidator : AbstractValidator<ReadCompanyByIdRequestDto>
    {
        public ReadByIdCompanyRequestDtoValidator()
        {
            Include(new ReadByIdRequestDtoValidator());
        }
    }
}
