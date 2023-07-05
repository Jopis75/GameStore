using Application.Dtos.Common.Interfaces;
using Application.Dtos.Companies;
using Application.Dtos.Products;
using Application.Dtos.Reviews;

namespace Application.Dtos.Consoles
{
    public class ReadAllConsoleResponseDto : IReadAllResponseDto
    {
        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }

        public ReadAllCompanyResponseDto? Developer { get; set; } = new();

        public int Id { get; set; }

        public string? ImageUri { get; set; }

        public string? Name { get; set; }

        public decimal? Price { get; set; }

        public List<ReadAllProductResponseDto>? Products { get; set; } = new();

        public DateTime? PurchaseDate { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public ReadAllReviewResponseDto? Review { get; set; } = new();

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public string? Url { get; set; }
    }
}
