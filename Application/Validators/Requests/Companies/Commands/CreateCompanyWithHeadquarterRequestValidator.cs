using Application.Features.Companies.Requests.Commands;
using Application.Interfaces.Persistance;
using Application.Validators.Dtos;
using FluentValidation;

namespace Application.Validators.Requests.Companies.Commands
{
    public class CreateCompanyWithHeadquarterRequestValidator : AbstractValidator<CreateCompanyWithHeadquarterRequest>
    {
        public CreateCompanyWithHeadquarterRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(createCompanyWithHeadquarterRequest => createCompanyWithHeadquarterRequest.CompanyDto)
                .NotNull()
                .SetValidator(createCompanyWithAddressRequest => new CompanyDtoValidator(unitOfWork));

            RuleFor(createCompanyWithHeadquarterRequest => createCompanyWithHeadquarterRequest.CompanyDto.Headquarter)
                .NotNull()
                .SetValidator(createCompanyWithHeadquarterRequest => new AddressDtoValidator(unitOfWork));
        }
    }
}
