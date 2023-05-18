using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Reviews
{
    public class ReadAllReviewResponseDto : IReadAllResponseDto
    {
        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }

        public int? Grade { get; set; }

        public int Id { get; set; }

        public DateTime? ReviewDate { get; set; }

        public string? ReviewText { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
