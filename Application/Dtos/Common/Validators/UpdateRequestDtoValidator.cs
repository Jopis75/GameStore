﻿using Application.Dtos.Common.Interfaces;
using FluentValidation;

namespace Application.Dtos.Common.Validators
{
    public class UpdateRequestDtoValidator : AbstractValidator<IUpdateRequestDto>
    {
        public UpdateRequestDtoValidator()
        {
            RuleFor(updateRequestDto => updateRequestDto.Id)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
