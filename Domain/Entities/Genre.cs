namespace GameStore.Domain.Entities
{
    public class Genre : EntityBase
    {
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public virtual ICollection<VideoGameGenre> VideoGameGenres { get; set; } = [];
    }
}
