using Application.Dtos.Common.Interfaces;
using FluentValidation;

namespace Application.Validators.Common
{
    public class DeleteRequestDtoValidator : AbstractValidator<DeleteRequestDto>
    {
        public DeleteRequestDtoValidator()
        {
            RuleFor(deleteRequestDto => deleteRequestDto.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
