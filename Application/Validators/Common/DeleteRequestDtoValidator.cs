using Application.Dtos.Common.Interfaces;
using FluentValidation;

namespace Application.Validators.Common
{
    public class DeleteRequestDtoValidator : AbstractValidator<IDeleteRequestDto>
    {
        public DeleteRequestDtoValidator()
        {
            RuleFor(deleteRequestDto => deleteRequestDto.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
