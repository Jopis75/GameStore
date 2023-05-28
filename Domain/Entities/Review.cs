namespace Domain.Entities
{
    public class Review : EntityBase
    {
        public int? Grade { get; set; }

        public DateTime? ReviewDate { get; set; }

        public string? ReviewText { get; set; }

        public virtual Product? VideoGame { get; set; }

        public int? VideoGameId { get; set; }
    }
}
