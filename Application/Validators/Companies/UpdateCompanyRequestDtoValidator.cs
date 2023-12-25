using Application.Dtos.Companies;
using Application.Validators.Common;
using FluentValidation;

namespace Application.Validators.Companies
{
    public class UpdateCompanyRequestDtoValidator : AbstractValidator<UpdateCompanyRequestDto>
    {
        public UpdateCompanyRequestDtoValidator()
        {
            Include(new UpdateRequestDtoValidator());

            RuleFor(updateCompanyRequestDto => updateCompanyRequestDto.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(updateCompanyRequestDto => updateCompanyRequestDto.TradeName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(updateCompanyRequestDto => updateCompanyRequestDto.HeadquarterId)
                .NotEqual(0)
                .WithMessage("{PropertyName} must not equal 0.");

            RuleFor(updateCompanyRequestDto => updateCompanyRequestDto.EmailAddress)
                .EmailAddress()
                .WithMessage("{PropertyName} is not valid.");

            RuleFor(updateCompanyRequestDto => updateCompanyRequestDto.PhoneNumber)
                .PhoneNumber()
                .WithMessage("{PropertyName} is not valid.");

            RuleFor(updateCompanyRequestDto => updateCompanyRequestDto.ParentCompanyId)
                .NotEqual(0)
                .WithMessage("{PropertyName} must not equal 0.");
        }
    }
}
