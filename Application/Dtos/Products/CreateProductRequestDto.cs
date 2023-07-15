using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Products
{
    public class CreateProductRequestDto : CreateRequestDto
    {
        public int? DeveloperId { get; set; }

        public string? ImageUri { get; set; }

        public decimal? Price { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public int? ReviewId { get; set; }

        public string? Title { get; set; }

        public TimeSpan? TotalTimePlayed { get; set; }

        public string? Url { get; set; }
    }
}
