namespace Domain.Entities
{
    public class VideoGame : Product
    {
        public virtual ICollection<ConsoleVideoGame> ConsoleVideoGames { get; set; } = new List<ConsoleVideoGame>();

        public string Title { get; set; } = default!;

        public TimeSpan TotalTimePlayed { get; set; }

        public virtual ICollection<VideoGameGenre> VideoGameGenres { get; set; } = new List<VideoGameGenre>();
    }
}
