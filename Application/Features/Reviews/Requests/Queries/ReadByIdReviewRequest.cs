using Application.Dtos.Common;
using Application.Dtos.Reviews;
using MediatR;

namespace Application.Features.Reviews.Requests.Queries
{
    public class ReadByIdReviewRequest : IRequest<HttpResponseDto<ReadByIdReviewResponseDto>>
    {
        public ReadByIdReviewRequestDto? ReadByIdReviewRequestDto { get; set; }
    }
}
