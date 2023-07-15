using Application.Dtos.Common;
using FluentValidation;

namespace Application.Validators.Common
{
    public class ReadByIdRequestDtoValidator : AbstractValidator<ReadByIdRequestDto>
    {
        public ReadByIdRequestDtoValidator()
        {
            RuleFor(readByIdRequestDto => readByIdRequestDto.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
