using Application.Interfaces.Persistance;
using Domain.Dtos;
using FluentValidation;

namespace Application.Validators.Dtos
{
    public class CompanyDtoValidator : AbstractValidator<CompanyDto>
    {
        public CompanyDtoValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(companyDto => companyDto.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (name, cancellationToken) =>
                {
                    var companyDtos = await unitOfWork.CompanyRepository.ReadByNameAsync(name, cancellationToken);
                    return companyDtos.Any() == false;
                })
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(companyDto => companyDto.TradeName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (tradeName, cancellationToken) =>
                {
                    var companyDtos = await unitOfWork.CompanyRepository.ReadByTradeNameAsync(tradeName, cancellationToken);
                    return companyDtos.Any() == false;
                })
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(companyDto => companyDto.HeadquarterId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(companyDto => companyDto.CompanyType)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(companyDto => companyDto.Industry)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(companyDto => companyDto.EmailAddress)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (emailAddress, cancellationToken) =>
                {
                    var companyDtos = await unitOfWork.CompanyRepository.ReadByEmailAddressAsync(emailAddress, cancellationToken);
                    return companyDtos.Any() == false;
                })
                .WithMessage("{PropertyName} must be unique.")
                .EmailAddress()
                .WithMessage("{PropertyName} is not valid.");

            RuleFor(companyDto => companyDto.PhoneNumber)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (phoneNumber, cancellationToken) =>
                {
                    var companyDtos = await unitOfWork.CompanyRepository.ReadByPhoneNumberAsync(phoneNumber, cancellationToken);
                    return companyDtos.Any() == false;
                })
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(companyDto => companyDto.ParentCompanyId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
