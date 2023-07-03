namespace Domain.Entities
{
    public class Product : EntityBase
    {
        public string? CoverImageUri { get; set; }

        public virtual Company? Developer { get; set; }

        public int? DeveloperId { get; set; }

        public decimal? Price { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public virtual Review? Review { get; set; }

        public int? ReviewId { get; set; }

        public string? Title { get; set; }

        public TimeSpan? TotalTimePlayed { get; set; }

        public string? Url { get; set; }
    }
}
