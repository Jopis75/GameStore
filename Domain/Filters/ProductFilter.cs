namespace Domain.Filters
{
    public abstract class ProductFilter : FilterBase
    {
        public int? DeveloperId { get; set; }

        public string? ImageUri { get; set; }

        public string? Name { get; set; }

        public decimal? Price { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string? Url { get; set; }
    }
}
