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
            RuleFor(createCompanyWithHeadquarterRequest => createCompanyWithHeadquarterRequest.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createCompanyWithHeadquarterRequest => createCompanyWithHeadquarterRequest.TradeName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createCompanyWithHeadquarterRequest => createCompanyWithHeadquarterRequest.Headquarter)
                .NotNull()
                .SetValidator(createCompanyWithHeadquarterRequest => new AddressDtoValidator(unitOfWork));

            RuleFor(createCompanyWithHeadquarterRequest => createCompanyWithHeadquarterRequest.CompanyType)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createCompanyWithHeadquarterRequest => createCompanyWithHeadquarterRequest.Industry)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createCompanyWithHeadquarterRequest => createCompanyWithHeadquarterRequest.EmailAddress)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .EmailAddress()
                .WithMessage("{PropertyName} is not valid.");

            RuleFor(createCompanyWithHeadquarterRequest => createCompanyWithHeadquarterRequest.PhoneNumber)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createCompanyWithHeadquarterRequest => createCompanyWithHeadquarterRequest.ParentCompanyId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
