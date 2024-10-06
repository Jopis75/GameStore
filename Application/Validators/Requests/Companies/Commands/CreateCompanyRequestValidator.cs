using Application.Features.Companies.Requests.Commands;
using FluentValidation;

namespace Application.Validators.Requests.Companies.Commands
{
    public class CreateCompanyRequestValidator : AbstractValidator<CreateCompanyRequest>
    {
        public CreateCompanyRequestValidator()
        {
            RuleFor(createCompanyRequest => createCompanyRequest.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createCompanyRequest => createCompanyRequest.TradeName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createCompanyRequest => createCompanyRequest.HeadquarterId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(createCompanyRequest => createCompanyRequest.CompanyType)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createCompanyRequest => createCompanyRequest.Industry)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createCompanyRequest => createCompanyRequest.EmailAddress)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .EmailAddress()
                .WithMessage("{PropertyName} is not valid.");

            RuleFor(createCompanyRequest => createCompanyRequest.PhoneNumber)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createCompanyRequest => createCompanyRequest.ParentCompanyId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
