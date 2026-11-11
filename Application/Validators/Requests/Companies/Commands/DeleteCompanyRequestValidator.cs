using FluentValidation;
using GameStore.Application.Features.Companies.Requests.Commands;

namespace GameStore.Application.Validators.Requests.Companies.Commands
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
