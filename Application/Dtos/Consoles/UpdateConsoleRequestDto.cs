using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Consoles
{
    public class UpdateConsoleRequestDto : IUpdateRequestDto
    {
        public int? DeveloperId { get; set; }

        public int Id { get; set; }

        public string? ImageUri { get; set; }

        public string? Name { get; set; }

        public decimal? Price { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public int? ReviewId { get; set; }

        public string? Url { get; set; }
    }
}
