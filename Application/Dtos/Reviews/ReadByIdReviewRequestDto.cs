using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Reviews
{
    public class ReadByIdReviewRequestDto : IReadByIdRequestDto
    {
        public int Id { get; set; }

        public ReadByIdReviewRequestDto(int id)
        {
            Id = id;
        }
    }
}
