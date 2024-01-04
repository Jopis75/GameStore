using Application.Dtos.Common;

namespace Application.Dtos.Reviews
{
    public class ReadReviewByVideoGameIdRequestDto : ReadRequestDto
    {
        public int VideoGameId { get; set; }
    }
}
