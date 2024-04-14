using Application.Dtos.Common;

namespace Application.Dtos.Consoles
{
    public class UpdateConsoleRequestDto : UpdateRequestDto
    {
        public int DeveloperId { get; set; }

        public string? ImageUri { get; set; }

        public string Name { get; set; } = default!;

        public decimal Price { get; set; }

        public DateTime PurchaseDate { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string? Url { get; set; }
    }
}
