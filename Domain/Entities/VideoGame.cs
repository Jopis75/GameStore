namespace Domain.Entities
{
    public class VideoGame : Product
    {
        public virtual ICollection<ConsoleVideoGame> ConsoleVideoGames { get; set; } = new List<ConsoleVideoGame>();

        public virtual Company Publisher { get; set; } = default!;

        public int PublisherId { get; set; }

        public string Title { get; set; } = default!;

        public TimeSpan TotalTimePlayed { get; set; }

        public virtual ICollection<Trophy> Trophies { get; set; } = new List<Trophy>();

        public virtual ICollection<VideoGameGenre> VideoGameGenres { get; set; } = new List<VideoGameGenre>();
    }
}
