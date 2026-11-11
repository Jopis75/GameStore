using FluentValidation;
using GameStore.Application.Features.Companies.Requests.Queries;

namespace GameStore.Application.Validators.Requests.Companies.Queries
{
    public class ReadCompanyByIdRequestValidator : AbstractValidator<ReadCompanyByIdRequest>
    {
        public ReadCompanyByIdRequestValidator()
        {
            RuleFor(readCompanyByIdRequest => readCompanyByIdRequest.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
