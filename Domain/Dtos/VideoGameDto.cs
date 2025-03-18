namespace Domain.Dtos
{
    public class VideoGameDto : ProductDto
    {
        public List<ConsoleVideoGameDto> ConsoleVideoGames { get; set; } = new();

        public CompanyDto Publisher { get; set; } = default!;

        public int PublisherId { get; set; }

        public string Title { get; set; } = default!;

        public TimeSpan TotalTimePlayed { get; set; }

        public List<TrophyDto> Trophies { get; set; } = new();

        public List<VideoGameGenreDto> VideoGameGenres { get; set; } = new();
    }
}
