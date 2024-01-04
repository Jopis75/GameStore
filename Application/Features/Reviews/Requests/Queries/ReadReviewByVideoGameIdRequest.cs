using Application.Dtos.Common;
using Application.Dtos.Reviews;
using MediatR;

namespace Application.Features.Reviews.Requests.Queries
{
    public class ReadReviewByVideoGameIdRequest : IRequest<HttpResponseDto<ReadReviewResponseDto>>
    {
        public ReadReviewByVideoGameIdRequestDto? ReadReviewByVideoGameIdRequestDto { get; set; }
    }
}
