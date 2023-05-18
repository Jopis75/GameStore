using Application.Dtos.Common.Validators;
using FluentValidation;

namespace Application.Dtos.Companies.Validators
{
    public class DeleteCompanyRequestDtoValidator : AbstractValidator<DeleteCompanyRequestDto>
    {
        public DeleteCompanyRequestDtoValidator()
        {
            Include(new DeleteRequestDtoValidator());
        }
    }
}
