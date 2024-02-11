using Application.Dtos.Consoles;
using Application.Interfaces.Persistance;
using FluentValidation;

namespace Application.Validators.Consoles
{
    public class CreateConsoleRequestDtoValidator : AbstractValidator<CreateConsoleRequestDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateConsoleRequestDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

            RuleFor(createConsoleRequestDto => createConsoleRequestDto.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .MustAsync(async (name, cancellation) =>
                {
                    var console = await _unitOfWork.ConsoleRepository.ReadByNameAsync(name);
                    return console.Id == 0;
                })
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(createConsoleRequestDto => createConsoleRequestDto.DeveloperId)
                .NotEqual(0)
                .WithMessage("{PropertyName} must not equal 0.");

            RuleFor(createConsoleRequestDto => createConsoleRequestDto.ReleaseDate)
                .LessThanOrEqualTo(createConsoleRequestDto => createConsoleRequestDto.PurchaseDate)
                .WithMessage("{PropertyName} must be less than or equal to {ComparisonProperty}.");

            RuleFor(createConsoleRequestDto => createConsoleRequestDto.PurchaseDate)
                .GreaterThanOrEqualTo(createConsoleRequestDto => createConsoleRequestDto.ReleaseDate)
                .WithMessage("{PropertyName} must be greater than or equal to {ComparisonProperty}.");

            RuleFor(createConsoleRequestDto => createConsoleRequestDto.Price)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
