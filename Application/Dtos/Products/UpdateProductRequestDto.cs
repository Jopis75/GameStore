using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Products
{
    public class UpdateProductRequestDto : IUpdateRequestDto
    {
        public string? CoverImageUri { get; set; }

        public string? Description { get; set; }

        public int? DeveloperId { get; set; }

        public int Id { get; set; }

        public decimal? Price { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public int? ReviewId { get; set; }

        public string? Title { get; set; }

        public TimeSpan? TotalTimePlayed { get; set; }
    }
}
