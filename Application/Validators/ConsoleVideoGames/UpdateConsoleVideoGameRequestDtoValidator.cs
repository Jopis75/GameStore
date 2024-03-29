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
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(createConsoleVideoGameRequestDto => createConsoleVideoGameRequestDto.VideoGameId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
