using Application.Dtos.Common;

namespace Application.Dtos.Reviews
{
    public class CreateReviewRequestDto : CreateRequestDto
    {
        public int? Grade { get; set; }

        public DateTime? ReviewDate { get; set; }

        public string? ReviewText { get; set; }

        public int? ProductId { get; set; }
    }
}
