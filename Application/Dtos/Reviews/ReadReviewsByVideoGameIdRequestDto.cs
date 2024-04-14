using Application.Dtos.Common;

namespace Application.Dtos.Reviews
{
    public class ReadReviewsByVideoGameIdRequestDto : RequestDto
    {
        public int VideoGameId { get; set; }
    }
}
