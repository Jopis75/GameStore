using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Reviews
{
    public class UpdateReviewRequestDto : UpdateRequestDto
    {
        public int? Grade { get; set; }

        public DateTime? ReviewDate { get; set; }

        public string? ReviewText { get; set; }

        public int? VideoGameId { get; set; }
    }
}
