using Application.Dtos.Common;
using Application.Dtos.Reviews;
using MediatR;

namespace Application.Features.Reviews.Requests.Queries
{
    public class ReadReviewByIdRequest : IRequest<HttpResponseDto<ReadReviewResponseDto>>
    {
        public ReadReviewByIdRequestDto? ReadReviewByIdRequestDto { get; set; }
    }
}
