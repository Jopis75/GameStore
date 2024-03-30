namespace Domain.Entities
{
    public abstract class Product : EntityBase
    {
        public virtual Company Developer { get; set; } = default!;

        public int DeveloperId { get; set; }

        public string? ImageUri { get; set; }

        public string Name { get; set; } = default!;

        public decimal Price { get; set; }

        public DateTime PurchaseDate { get; set; }

        public DateTime ReleaseDate { get; set; }

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        public string? Url { get; set; }
    }
}
