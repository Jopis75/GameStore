
using Application.Dtos.ConsoleVideoGames;
using FluentValidation;

namespace Application.Validators.ConsoleVideoGames
{
    public class CreateConsoleVideoGameRequestDtoValidator : AbstractValidator<CreateConsoleVideoGameRequestDto>
    {
        public CreateConsoleVideoGameRequestDtoValidator()
        {
            RuleFor(createConsoleVideoGameRequestDto => createConsoleVideoGameRequestDto.ConsoleId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(createConsoleVideoGameRequestDto => createConsoleVideoGameRequestDto.VideoGameId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
