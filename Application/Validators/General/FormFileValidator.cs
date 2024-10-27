using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Validators.General
{
    public class FormFileValidator : AbstractValidator<IFormFile>
    {
        public FormFileValidator()
        {
            RuleFor(formFile => formFile.ContentDisposition)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(formFile => formFile.ContentType)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(formFile => formFile.FileName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(formFile => formFile.Length)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
