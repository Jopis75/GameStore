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
                    var companies = await _unitOfWork.CompanyRepository.ReadByNameAsync(name);
                    return companies.Count() == 0;
                })
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(createCompanyRequestDto => createCompanyRequestDto.TradeName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (tradeName, cancellation) =>
                {
                    var companies = await _unitOfWork.CompanyRepository.ReadByTradeNameAsync(tradeName);
                    return companies.Count() == 0;
                })
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(createCompanyRequestDto => createCompanyRequestDto.HeadquarterId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(createCompanyRequestDto => createCompanyRequestDto.EmailAddress)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .EmailAddress()
                .WithMessage("{PropertyName} is not valid.")
                .MustAsync(async (emailAddress, cancellation) =>
                {
                    var companies = await _unitOfWork.CompanyRepository.ReadByEmailAddressAsync(emailAddress);
                    return companies.Count() == 0;
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
                    var companies = await _unitOfWork.CompanyRepository.ReadByPhoneNumberAsync(phoneNumber ?? String.Empty);
                    return companies.Count() == 0;
                })
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(createCompanyRequestDto => createCompanyRequestDto.ParentCompanyId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
