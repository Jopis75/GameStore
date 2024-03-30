namespace Domain.Entities
{
    public class Review : EntityBase
    {
        public virtual Console? Console { get; set; }

        public int? ConsoleId { get; set; }

        // Grade between 0 and 100.
        public int Grade { get; set; }

        public DateTime ReviewDate { get; set; }

        public string ReviewText { get; set; } = default!;

        public virtual VideoGame? VideoGame { get; set; }

        public int? VideoGameId { get; set; }
    }
}
