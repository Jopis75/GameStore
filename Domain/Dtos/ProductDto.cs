namespace Domain.Dtos
{
    public abstract class ProductDto : DtoBase
    {
        public CompanyDto Developer { get; set; } = default!;

        public int DeveloperId { get; set; }

        public string? ImageUri { get; set; }

        public string Name { get; set; } = default!;

        public decimal Price { get; set; }

        public DateTime PurchaseDate { get; set; }

        public DateTime ReleaseDate { get; set; }

        public List<ReviewDto> Reviews { get; set; } = new();

        public string? Url { get; set; }
    }
}
