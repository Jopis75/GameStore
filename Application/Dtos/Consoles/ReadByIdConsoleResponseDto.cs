using Application.Dtos.Common;
using Application.Dtos.Companies;
using Application.Dtos.Reviews;
using Application.Dtos.VideoGames;

namespace Application.Dtos.Consoles
{
    public class ReadByIdConsoleResponseDto : ReadByIdResponseDto
    {
        public ReadByIdCompanyResponseDto Developer { get; set; } = new();

        public string ImageUri { get; set; } = default!;

        public string Name { get; set; } = default!;

        public decimal Price { get; set; }

        public DateTime PurchaseDate { get; set; }

        public DateTime ReleaseDate { get; set; }

        public ReadByIdReviewResponseDto? Review { get; set; }

        public string Url { get; set; } = default!;

        public List<ReadByIdVideoGameResponseDto> VideoGames { get; set; } = new();
    }
}
