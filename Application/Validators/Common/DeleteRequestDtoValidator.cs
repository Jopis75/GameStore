using Application.Dtos.Common;
using FluentValidation;

namespace Application.Validators.Common
{
    public class DeleteRequestDtoValidator : AbstractValidator<DeleteRequestDto>
    {
        public DeleteRequestDtoValidator()
        {
            RuleFor(deleteRequestDto => deleteRequestDto.DeletedBy)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(deleteRequestDto => deleteRequestDto.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
