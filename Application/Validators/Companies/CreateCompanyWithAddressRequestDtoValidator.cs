using Application.Dtos.Companies;
using Application.Interfaces.Persistance;
using Application.Validators.Common;
using FluentValidation;

namespace Application.Validators.Companies
{
    public class CreateCompanyWithAddressRequestDtoValidator : AbstractValidator<CreateCompanyWithAddressRequestDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCompanyWithAddressRequestDtoValidator(IUnitOfWork unitOfWork)
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

            RuleFor(createCompanyRequestDto => createCompanyRequestDto.EmailAddress)
                .EmailAddress()
                .WithMessage("{PropertyName} is not valid.")
                .MustAsync(async (emailAddress, cancellation) =>
                {
                    var company = await _unitOfWork.CompanyRepository.ReadByEmailAddressAsync(emailAddress);
                    return company.Id == 0;
                })
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(createCompanyRequestDto => createCompanyRequestDto.PhoneNumber)
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

            RuleFor(createAddressRequestDto => createAddressRequestDto.StreetAddress)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (streetAddress, cancellation) =>
                {
                    var address = await _unitOfWork.AddressRepository.ReadByStreetAddressAsync(streetAddress);
                    return address.Id == 0;
                })
                .WithMessage("{PropertyName} must be unique.");

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
