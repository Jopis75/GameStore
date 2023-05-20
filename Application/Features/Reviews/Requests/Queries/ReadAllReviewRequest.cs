using Application.Dtos.Common;
using Application.Dtos.Reviews;
using MediatR;

namespace Application.Features.Reviews.Requests.Queries
{
    public class ReadAllReviewRequest : IRequest<HttpResponseDto<ReadAllReviewResponseDto>>
    {
        public ReadAllReviewRequestDto? ReadAllReviewRequestDto { get; set; }
    }
}
