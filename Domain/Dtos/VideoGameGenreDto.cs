namespace Domain.Dtos
{
    public class VideoGameGenreDto : DtoBase
    {
        public GenreDto Genre { get; set; } = new();

        public int GenreId { get; set; }

        public VideoGameDto VideoGame { get; set; } = new();

        public int VideoGameId { get; set; }
    }
}
