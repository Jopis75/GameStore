using Application.Features.Companies.Requests.Commands;
using FluentValidation;

namespace Application.Validators.Requests.Companies.Commands
{
    public class DeleteCompanyRequestValidator : AbstractValidator<DeleteCompanyRequest>
    {
        public DeleteCompanyRequestValidator()
        {
            RuleFor(deleteCompanyRequest => deleteCompanyRequest.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
