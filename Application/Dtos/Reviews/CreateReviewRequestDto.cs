using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Reviews
{
    public class CreateReviewRequestDto : ICreateRequestDto
    {
        public int? Grade { get; set; }

        public int? ProductId { get; set; }

        public DateTime? ReviewDate { get; set; }

        public string? ReviewText { get; set; }
    }
}
