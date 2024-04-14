using Application.Dtos.Common;
using Application.Dtos.Companies;
using Application.Dtos.Reviews;

namespace Application.Dtos.VideoGames
{
    public class ReadVideoGameResponseDto : ReadResponseDto
    {
        public ReadCompanyResponseDto Developer { get; set; } = new();

        public string? ImageUri { get; set; }

        public decimal Price { get; set; }

        public DateTime PurchaseDate { get; set; }

        public DateTime ReleaseDate { get; set; }

        public List<ReadReviewResponseDto> Reviews { get; set; } = new();

        public string Title { get; set; } = default!;

        public TimeSpan TotalTimePlayed { get; set; }

        public string? Url { get; set; }
    }
}
