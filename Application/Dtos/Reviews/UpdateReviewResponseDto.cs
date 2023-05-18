using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Reviews
{
    public class UpdateReviewResponseDto : IUpdateResponseDto
    {
        public int Id { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
