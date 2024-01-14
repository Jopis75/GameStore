using Application.Dtos.Identity;
using FluentValidation;

namespace Application.Validators.Identity
{
    public class RegistrationRequestDtoValidator : AbstractValidator<RegistrationRequestDto>
    {
        public RegistrationRequestDtoValidator()
        {
            RuleFor(registrationRequestDto => registrationRequestDto.UserName)
                .NotNull()
                .NotEmpty();

            RuleFor(registrationRequestDto => registrationRequestDto.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(16);

            RuleFor(registrationRequestDto => registrationRequestDto.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();

            RuleFor(registrationRequestDto => registrationRequestDto.FirstName)
                .NotNull()
                .NotEmpty();

            RuleFor(registrationRequestDto => registrationRequestDto.LastName)
                .NotNull()
                .NotEmpty();

            RuleFor(registrationRequestDto => registrationRequestDto.Role)
                .NotNull()
                .NotEmpty();
        }
    }
}
