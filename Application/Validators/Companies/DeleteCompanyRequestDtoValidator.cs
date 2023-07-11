using Application.Dtos.Companies;
using Application.Validators.Common;
using FluentValidation;

namespace Application.Validators.Companies
{
    public class DeleteCompanyRequestDtoValidator : AbstractValidator<DeleteCompanyRequestDto>
    {
        public DeleteCompanyRequestDtoValidator()
        {
            Include(new DeleteRequestDtoValidator());
        }
    }
}
