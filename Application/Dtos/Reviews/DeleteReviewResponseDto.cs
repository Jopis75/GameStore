using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Reviews
{
    public class DeleteReviewResponseDto : IDeleteResponseDto
    {
        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }

        public int Id { get; set; }
    }
}
