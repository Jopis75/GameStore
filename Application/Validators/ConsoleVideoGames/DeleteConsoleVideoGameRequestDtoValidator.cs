using Application.Dtos.ConsoleVideoGames;
using Application.Validators.Common;
using FluentValidation;

namespace Application.Validators.ConsoleVideoGames
{
    public class DeleteConsoleVideoGameRequestDtoValidator : AbstractValidator<DeleteConsoleVideoGameRequestDto>
    {
        public DeleteConsoleVideoGameRequestDtoValidator()
        {
            Include(new DeleteRequestDtoValidator());
        }
    }
}
