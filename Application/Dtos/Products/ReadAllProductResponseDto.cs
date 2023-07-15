using Application.Dtos.Common.Interfaces;
using Application.Dtos.Companies;
using Application.Dtos.Reviews;

namespace Application.Dtos.Products
{
    public class ReadAllProductResponseDto : ReadAllResponseDto
    {
        public ReadAllCompanyResponseDto? Developer { get; set; } = new();

        public string? ImageUri { get; set; }

        public decimal? Price { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public ReadAllReviewResponseDto? Review { get; set; } = new();

        public string? Title { get; set; }

        public TimeSpan? TotalTimePlayed { get; set; }

        public string? Url { get; set; }
    }
}
