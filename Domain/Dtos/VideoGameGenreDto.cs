namespace Domain.Dtos
{
    public class VideoGameGenreDto : DtoBase
    {
        public GenreDto Genre { get; set; } = default!;

        public int GenreId { get; set; }

        public VideoGameDto VideoGame { get; set; } = default!;

        public int VideoGameId { get; set; }
    }
}
