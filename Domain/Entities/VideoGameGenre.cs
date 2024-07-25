namespace Domain.Entities
{
    public class VideoGameGenre : EntityBase
    {
        public virtual Genre Genre { get; set; } = default!;

        public int GenreId { get; set; }

        public virtual VideoGame VideoGame { get; set; } = default!;

        public int VideoGameId { get; set; }
    }
}
