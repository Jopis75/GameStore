using Application.Dtos.Common.Interfaces;
using Application.Dtos.Companies;
using Application.Dtos.Reviews;

namespace Application.Dtos.Products
{
    public class ReadAllProductResponseDto : IReadAllResponseDto
    {
        public string? CoverImageUri { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }

        public ReadAllCompanyResponseDto? Developer { get; set; } = new();

        public int Id { get; set; }

        public decimal? Price { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public ReadAllReviewResponseDto? Review { get; set; } = new();

        public string? Title { get; set; }

        public TimeSpan? TotalTimePlayed { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public string? Url { get; set; }
    }
}
