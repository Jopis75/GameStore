using Application.Dtos.Common;

namespace Application.Dtos.Reviews
{
    public class CreateReviewRequestDto : CreateRequestDto
    {
        public int? ConsoleId { get; set; }

        public int Grade { get; set; }

        public DateTime ReviewDate { get; set; }

        public string ReviewText { get; set; } = default!;

        public int? VideoGameId { get; set; }
    }
}
