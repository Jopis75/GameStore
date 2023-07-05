namespace Domain.Entities
{
    public class Console : EntityBase
    {
        public virtual ICollection<ConsoleProduct>? ConsoleProducts { get; set; } = new List<ConsoleProduct>();

        public virtual Company? Developer { get; set; }

        public int? DeveloperId { get; set; }

        public string? ImageUri { get; set; }

        public string? Name { get; set; }

        public decimal? Price { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public virtual Review? Review { get; set; }

        public int? ReviewId { get; set; }

        public string? Url { get; set; }
    }
}
