using FluentValidation;

namespace Application.Dtos.Products.Validators
{
    public class CreateProductRequestDtoValidator : AbstractValidator<CreateProductRequestDto>
    {
        public CreateProductRequestDtoValidator()
        {
            RuleFor(createProductRequestDto => createProductRequestDto.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(createProductRequestDto => createProductRequestDto.DeveloperId)
                .NotEqual(0)
                .WithMessage("{PropertyName} must not equal 0.");

            RuleFor(createProductRequestDto => createProductRequestDto.ReleaseDate)
                .LessThanOrEqualTo(createProductRequestDto => createProductRequestDto.PurchaseDate)
                .WithMessage("{PropertyName} must be less than or equal to {ComparisonProperty}.");

            RuleFor(createProductRequestDto => createProductRequestDto.PurchaseDate)
                .GreaterThanOrEqualTo(createProductRequestDto => createProductRequestDto.ReleaseDate)
                .WithMessage("{PropertyName} must be greater than or equal to {ComparisonProperty}.");

            RuleFor(createProductRequestDto => createProductRequestDto.Price)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(createProductRequestDto => createProductRequestDto.TotalTimePlayed)
                .GreaterThanOrEqualTo(TimeSpan.Zero)
                .WithMessage("{PropertyName} must be greater than or equal to " + $"{TimeSpan.Zero}.");
        }
    }
}
