using Application.Dtos.ConsoleVideoGames;
using Application.Validators.Common;
using FluentValidation;

namespace Application.Validators.ConsoleVideoGames
{
    public class UpdateConsoleVideoGameRequestDtoValidator : AbstractValidator<UpdateConsoleVideoGameRequestDto>
    {
        public UpdateConsoleVideoGameRequestDtoValidator()
        {
            Include(new UpdateRequestDtoValidator());

            RuleFor(createConsoleVideoGameRequestDto => createConsoleVideoGameRequestDto.ConsoleId)
                .NotEqual(0)
                .WithMessage("{PropertyName} must not equal 0.");

            RuleFor(createConsoleVideoGameRequestDto => createConsoleVideoGameRequestDto.VideoGameId)
                .NotEqual(0)
                .WithMessage("{PropertyName} must not equal 0.");
        }
    }
}
