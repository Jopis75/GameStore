using Application.Dtos.Common;
using Application.Dtos.Companies;
using Application.Dtos.Reviews;

namespace Application.Dtos.VideoGames
{
    public class ReadByIdVideoGameResponseDto : ReadByIdResponseDto
    {
        public ReadByIdCompanyResponseDto Developer { get; set; } = new();

        public string ImageUri { get; set; } = default!;

        public decimal Price { get; set; }

        public DateTime PurchaseDate { get; set; }

        public DateTime ReleaseDate { get; set; }

        public ReadByIdReviewResponseDto? Review { get; set; }

        public string Title { get; set; } = default!;

        public TimeSpan TotalTimePlayed { get; set; }

        public string Url { get; set; } = default!;
    }
}
