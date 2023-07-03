using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Reviews
{
    public class DeleteReviewRequestDto : IDeleteRequestDto
    {
        public int Id { get; set; }
    }
}
