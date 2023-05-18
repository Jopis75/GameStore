using Application.Dtos.Common.Interfaces;
using FluentValidation;

namespace Application.Dtos.Common.Validators
{
    public class ReadByIdRequestDtoValidator : AbstractValidator<IReadByIdRequestDto>
    {
        public ReadByIdRequestDtoValidator()
        {
            RuleFor(readByIdRequestDto => readByIdRequestDto.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
