using Application.Dtos.Companies;
using Application.Interfaces.Persistance;
using Application.Validators.Common;
using FluentValidation;

namespace Application.Validators.Companies
{
    public class CreateCompanyRequestDtoValidator : AbstractValidator<CreateCompanyRequestDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCompanyRequestDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

            RuleFor(createCompanyRequestDto => createCompanyRequestDto.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (name, cancellation) =>
                {
                    var company = await _unitOfWork.CompanyRepository.ReadByNameAsync(name);
                    return company.Id == 0;
                })
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(createCompanyRequestDto => createCompanyRequestDto.TradeName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (tradeName, cancellation) =>
                {
                    var company = await _unitOfWork.CompanyRepository.ReadByTradeNameAsync(tradeName);
                    return company.Id == 0;
                })
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(createCompanyRequestDto => createCompanyRequestDto.HeadquarterId)
                .NotEqual(0)
                .WithMessage("{PropertyName} must not equal 0.");

            RuleFor(createCompanyRequestDto => createCompanyRequestDto.EmailAddress)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .EmailAddress()
                .WithMessage("{PropertyName} is not valid.")
                .MustAsync(async (emailAddress, cancellation) =>
                {
                    var company = await _unitOfWork.CompanyRepository.ReadByEmailAddressAsync(emailAddress);
                    return company.Id == 0;
                })
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(createCompanyRequestDto => createCompanyRequestDto.PhoneNumber)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .PhoneNumber()
                .WithMessage("{PropertyName} is not valid.")
                .MustAsync(async (phoneNumber, cancellation) =>
                {
                    var company = await _unitOfWork.CompanyRepository.ReadByPhoneNumberAsync(phoneNumber ?? String.Empty);
                    return company.Id == 0;
                })
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(createCompanyRequestDto => createCompanyRequestDto.ParentCompanyId)
                .NotEqual(0)
                .WithMessage("{PropertyName} must not equal 0.");
        }
    }
}
