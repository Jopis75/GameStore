﻿using Application.Features.Genres.Requests.Commands;
using FluentValidation;

namespace Application.Validators.Requests.Genres.Commands
{
    public class CreateGenreRequestValidator : AbstractValidator<CreateGenreRequest>
    {
        public CreateGenreRequestValidator()
        {
            RuleFor(createGenreRequest => createGenreRequest.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}
