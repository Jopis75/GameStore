using Application.Dtos.Identity;
using FluentValidation;

namespace Application.Validators.Identity
{
    public class ReadUserByUserIdRequestDtoValidator : AbstractValidator<ReadUserByUserIdRequestDto>
    {
        public ReadUserByUserIdRequestDtoValidator()
        {
            RuleFor(readUserByUserIdRequestDto => readUserByUserIdRequestDto.UserId)
                .NotNull()
                .NotEmpty();
            // ToDo: Check GUID.
        }
    }
}
