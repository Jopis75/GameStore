using Application.Dtos.Identity;
using FluentValidation;

namespace Application.Validators.Identity
{
    public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(16);
        }
    }
}
