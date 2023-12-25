using Application.Dtos.VideoGames;
using Application.Validators.Common;
using FluentValidation;

namespace Application.Validators.VideoGames
{
    public class DeleteVideoGameRequestDtoValidator : AbstractValidator<DeleteVideoGameRequestDto>
    {
        public DeleteVideoGameRequestDtoValidator()
        {
            Include(new DeleteRequestDtoValidator());
        }
    }
}
