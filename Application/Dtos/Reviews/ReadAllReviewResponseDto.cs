using Application.Dtos.Common;
using Application.Dtos.Consoles;
using Application.Dtos.VideoGames;

namespace Application.Dtos.Reviews
{
    public class ReadAllReviewResponseDto : ReadAllResponseDto
    {
        public ReadAllConsoleResponseDto? Console { get; set; }

        public int Grade { get; set; }

        public DateTime ReviewDate { get; set; }

        public string ReviewText { get; set; } = default!;

        public ReadAllVideoGameResponseDto? VideoGame { get; set; }
    }
}
