using Application.Features.Companies.Requests.Commands;
using Application.Interfaces.Persistance;
using Application.Validators.Dtos;
using FluentValidation;

namespace Application.Validators.Requests.Companies.Commands
{
    public class CreateCompanyRequestValidator : AbstractValidator<CreateCompanyRequest>
    {
        public CreateCompanyRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(createCompanyRequest => createCompanyRequest.CompanyDto)
                .NotNull()
                .SetValidator(createCompanyRequest => new CompanyDtoValidator(unitOfWork));
        }
    }
}
