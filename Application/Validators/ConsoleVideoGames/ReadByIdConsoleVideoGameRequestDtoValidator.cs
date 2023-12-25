using Application.Dtos.ConsoleVideoGames;
using Application.Validators.Common;
using FluentValidation;

namespace Application.Validators.ConsoleVideoGames
{
    public class ReadByIdConsoleVideoGameRequestDtoValidator : AbstractValidator<ReadByIdConsoleVideoGameRequestDto>
    {
        public ReadByIdConsoleVideoGameRequestDtoValidator()
        {
            Include(new ReadByIdRequestDtoValidator());
        }
    }
}
