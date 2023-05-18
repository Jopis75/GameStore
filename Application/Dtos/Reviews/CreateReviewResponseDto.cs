using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Reviews
{
    public class CreateReviewResponseDto : ICreateResponseDto
    {
        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public int Id { get; set; }
    }
}
