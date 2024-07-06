using Application.Features.Companies.Requests.Queries;
using FluentValidation;

namespace Application.Validators.Requests.Companies.Queries
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
