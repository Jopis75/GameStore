
using Application.Dtos.ConsoleVideoGames;
using FluentValidation;

namespace Application.Validators.ConsoleVideoGames
{
    public class CreateConsoleVideoGameRequestDtoValidator : AbstractValidator<CreateConsoleVideoGameRequestDto>
    {
        public CreateConsoleVideoGameRequestDtoValidator()
        {
            RuleFor(createConsoleVideoGameRequestDto => createConsoleVideoGameRequestDto.ConsoleId)
                .NotEqual(0)
                .WithMessage("{PropertyName} must not equal 0.");

            RuleFor(createConsoleVideoGameRequestDto => createConsoleVideoGameRequestDto.VideoGameId)
                .NotEqual(0)
                .WithMessage("{PropertyName} must not equal 0.");
        }
    }
}
