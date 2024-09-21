using Application.Features.Companies.Requests.Commands;
using Application.Interfaces.Persistance;
using FluentValidation;

namespace Application.Validators.Requests.Companies.Commands
{
    public class UpdateCompanyRequestValidator : AbstractValidator<UpdateCompanyRequest>
    {
        public UpdateCompanyRequestValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(updateCompanyRequest => updateCompanyRequest.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(updateCompanyRequest => updateCompanyRequest.TradeName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(updateCompanyRequest => updateCompanyRequest.HeadquarterId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(updateCompanyRequest => updateCompanyRequest.CompanyType)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(updateCompanyRequest => updateCompanyRequest.Industry)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(updateCompanyRequest => updateCompanyRequest.EmailAddress)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .EmailAddress()
                .WithMessage("{PropertyName} is not valid.");

            RuleFor(updateCompanyRequest => updateCompanyRequest.PhoneNumber)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(updateCompanyRequest => updateCompanyRequest.ParentCompanyId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
