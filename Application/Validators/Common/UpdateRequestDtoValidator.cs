using Application.Dtos.Common.Interfaces;
using FluentValidation;

namespace Application.Validators.Common
{
    public class UpdateRequestDtoValidator : AbstractValidator<UpdateRequestDto>
    {
        public UpdateRequestDtoValidator()
        {
            RuleFor(updateRequestDto => updateRequestDto.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
