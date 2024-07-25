using Domain.Dtos;
using FluentValidation;

namespace Application.Validators.Dtos
{
    public class VideoGameGenreDtoValidator : AbstractValidator<VideoGameGenreDto>
    {
        public VideoGameGenreDtoValidator()
        {
            RuleFor(videoGameGenreDto => videoGameGenreDto.VideoGameId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(videoGameGenreDto => videoGameGenreDto.GenreId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0.");
        }
    }
}
