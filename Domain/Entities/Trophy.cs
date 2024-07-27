namespace Domain.Entities
{
    public class Trophy : EntityBase
    {
        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string IconUrl { get; set; } = default!;

        public TrophyValue TrophyValue { get; set; }

        public virtual VideoGame VideoGame { get; set; } = default!;

        public int VideoGameId { get; set; }
    }
}
