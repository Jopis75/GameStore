using Application.Dtos.Companies;
using Application.Interfaces.Persistance;
using Application.Validators.Common;
using FluentValidation;

namespace Application.Validators.Companies
{
    public class UpdateCompanyRequestDtoValidator : AbstractValidator<UpdateCompanyRequestDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCompanyRequestDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

            Include(new UpdateRequestDtoValidator());

            RuleFor(updateCompanyRequestDto => updateCompanyRequestDto.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (name, cancellation) =>
                {
                    var company = await _unitOfWork.CompanyRepository.ReadByNameAsync(name);
                    return company.Id == 0;
                })
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(updateCompanyRequestDto => updateCompanyRequestDto.TradeName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (tradeName, cancellation) =>
                {
                    var company = await _unitOfWork.CompanyRepository.ReadByTradeNameAsync(tradeName);
                    return company.Id == 0;
                })
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(updateCompanyRequestDto => updateCompanyRequestDto.HeadquarterId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(updateCompanyRequestDto => updateCompanyRequestDto.EmailAddress)
                .EmailAddress()
                .WithMessage("{PropertyName} is not valid.")
                .MustAsync(async (emailAddress, cancellation) =>
                {
                    var company = await _unitOfWork.CompanyRepository.ReadByEmailAddressAsync(emailAddress);
                    return company.Id == 0;
                })
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(updateCompanyRequestDto => updateCompanyRequestDto.PhoneNumber)
                .PhoneNumber()
                .WithMessage("{PropertyName} is not valid.")
                .MustAsync(async (phoneNumber, cancellation) =>
                {
                    var company = await _unitOfWork.CompanyRepository.ReadByPhoneNumberAsync(phoneNumber ?? String.Empty);
                    return company.Id == 0;
                })
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(updateCompanyRequestDto => updateCompanyRequestDto.ParentCompanyId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
