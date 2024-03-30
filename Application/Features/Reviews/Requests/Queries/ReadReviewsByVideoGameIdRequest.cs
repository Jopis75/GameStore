using Application.Dtos.Common;
using Application.Dtos.Reviews;
using MediatR;

namespace Application.Features.Reviews.Requests.Queries
{
    public class ReadReviewsByVideoGameIdRequest : IRequest<HttpResponseDto<List<ReadReviewResponseDto>>>
    {
        public ReadReviewsByVideoGameIdRequestDto? ReadReviewsByVideoGameIdRequestDto { get; set; }
    }
}
