using Application.Dtos.Common;

namespace Application.Dtos.VideoGames
{
    public class CreateVideoGameRequestDto : CreateRequestDto
    {
        public int DeveloperId { get; set; }

        public string? ImageUri { get; set; }

        public decimal Price { get; set; }

        public DateTime PurchaseDate { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Title { get; set; } = default!;

        public TimeSpan TotalTimePlayed { get; set; }

        public string? Url { get; set; }
    }
}
