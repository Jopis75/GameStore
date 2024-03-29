using Application.Dtos.Identity;
using FluentValidation;

namespace Application.Validators.Identity
{
    public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestDtoValidator()
        {
            RuleFor(loginRequestDto => loginRequestDto.UserName)
                .NotNull()
                .NotEmpty();

            RuleFor(loginRequestDto => loginRequestDto.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(16);
        }
    }
}
