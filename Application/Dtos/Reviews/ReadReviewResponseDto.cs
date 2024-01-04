using Application.Dtos.Common;
using Application.Dtos.Consoles;
using Application.Dtos.VideoGames;

namespace Application.Dtos.Reviews
{
    public class ReadReviewResponseDto : ReadResponseDto
    {
        public ReadConsoleResponseDto? Console { get; set; }

        public int Grade { get; set; }

        public DateTime ReviewDate { get; set; }

        public string ReviewText { get; set; } = default!;

        public ReadVideoGameResponseDto? VideoGame { get; set; }
    }
}
