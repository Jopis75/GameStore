namespace Domain.Dtos
{
    public class VideoGameDto : ProductDto
    {
        public List<ConsoleVideoGameDto> ConsoleVideoGames { get; set; } = new();

        public string Title { get; set; } = default!;

        public TimeSpan TotalTimePlayed { get; set; }
    }
}
