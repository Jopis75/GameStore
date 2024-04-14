using Application.Dtos.Common;
using FluentValidation;

namespace Application.Validators.Common
{
    public class CreateRequestDtoValidator : AbstractValidator<CreateRequestDto>
    {
        public CreateRequestDtoValidator()
        {
            RuleFor(createRequestDto => createRequestDto.CreatedBy)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}
