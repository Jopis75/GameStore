namespace Domain.Entities
{
    public class Review : EntityBase
    {
        public int? Grade { get; set; }

        public virtual Product? Product { get; set; }

        public int? ProductId { get; set; }

        public DateTime? ReviewDate { get; set; }

        public string? ReviewText { get; set; }
    }
}
