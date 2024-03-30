using Application.Dtos.Common;

namespace Application.Dtos.Reviews
{
    public class ReadReviewsByVideoGameIdRequestDto : ReadRequestDto
    {
        public int VideoGameId { get; set; }
    }
}
