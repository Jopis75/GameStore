using FluentValidation;

namespace Application.Dtos.Companies.Validators
{
    public class CreateCompanyRequestDtoValidator : AbstractValidator<CreateCompanyRequestDto>
    {
        public CreateCompanyRequestDtoValidator()
        {
            RuleFor(createCompanyRequestDto => createCompanyRequestDto.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createCompanyRequestDto => createCompanyRequestDto.TradeName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createCompanyRequestDto => createCompanyRequestDto.HeadquartersId)
                .NotEqual(0)
                .WithMessage("{PropertyName} must not equal 0.");

            RuleFor(createCompanyRequestDto => createCompanyRequestDto.EmailAddress)
                .EmailAddress()
                .WithMessage("{PropertyName} is not valid.");

            //RuleFor(createCompanyRequestDto => createCompanyRequestDto.PhoneNumber)

            RuleFor(createCompanyRequestDto => createCompanyRequestDto.ParentCompanyId)
                .NotEqual(0)
                .WithMessage("{PropertyName} must not equal 0.");
        }
    }
}
