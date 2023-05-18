using Application.Dtos.Common.Validators;
using FluentValidation;

namespace Application.Dtos.Companies.Validators
{
    public class UpdateCompanyRequestDtoValidator : AbstractValidator<UpdateCompanyRequestDto>
    {
        public UpdateCompanyRequestDtoValidator()
        {
            Include(new UpdateRequestDtoValidator());

            RuleFor(createCompanyRequestDto => createCompanyRequestDto.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createCompanyRequestDto => createCompanyRequestDto.TradeName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createCompanyRequestDto => createCompanyRequestDto.Founded)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(createCompanyRequestDto => createCompanyRequestDto.NumberOfEmployees)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(createCompanyRequestDto => createCompanyRequestDto.HeadOfficeId)
                .NotEqual(0)
                .WithMessage("{PropertyName} must not equal 0.");

            RuleFor(createCompanyRequestDto => createCompanyRequestDto.ParentCompanyId)
                .NotEqual(0)
                .WithMessage("{PropertyName} must not equal 0.");
        }
    }
}
