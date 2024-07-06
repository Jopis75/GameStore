using Application.Features.Companies.Requests.Commands;
using Application.Interfaces.Persistance;
using Application.Validators.Dtos;
using FluentValidation;

namespace Application.Validators.Requests.Companies.Commands
{
    public class UpdateCompanyRequestValidator : AbstractValidator<UpdateCompanyRequest>
    {
        public UpdateCompanyRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(updateCompanyRequest => updateCompanyRequest.CompanyDto)
                .NotNull()
                .SetValidator(updateCompanyRequest => new CompanyDtoValidator(unitOfWork));
        }
    }
}
