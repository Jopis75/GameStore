using Application.Dtos.VideoGames;
using Application.Validators.Common;
using FluentValidation;

namespace Application.Validators.VideoGames
{
    public class ReadByIdVideoGameRequestDtoValidator : AbstractValidator<ReadVideoGameByIdRequestDto>
    {
        public ReadByIdVideoGameRequestDtoValidator()
        {
            Include(new ReadByIdRequestDtoValidator());
        }
    }
}
