using Application.Dtos.Companies;
using Application.Validators.Common;
using FluentValidation;

namespace Application.Validators.Companies
{
    public class CreateCompanyWithAddressRequestDtoValidator : AbstractValidator<CreateCompanyWithAddressRequestDto>
    {
        public CreateCompanyWithAddressRequestDtoValidator()
        {
            RuleFor(createCompanyRequestDto => createCompanyRequestDto.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createCompanyRequestDto => createCompanyRequestDto.TradeName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createCompanyRequestDto => createCompanyRequestDto.EmailAddress)
                .EmailAddress()
                .WithMessage("{PropertyName} is not valid.");

            RuleFor(createCompanyRequestDto => createCompanyRequestDto.PhoneNumber)
                .PhoneNumber()
                .WithMessage("{PropertyName} is not valid.");

            RuleFor(createCompanyRequestDto => createCompanyRequestDto.ParentCompanyId)
                .NotEqual(0)
                .WithMessage("{PropertyName} must not equal 0.");

            RuleFor(createAddressRequestDto => createAddressRequestDto.StreetAddress)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createAddressRequestDto => createAddressRequestDto.City)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createAddressRequestDto => createAddressRequestDto.State)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createAddressRequestDto => createAddressRequestDto.PostalCode)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}
