using Application.Dtos.Common;
using Application.Dtos.Reviews;
using MediatR;

namespace Application.Features.Reviews.Requests.Commands
{
    public class DeleteReviewRequest : IRequest<HttpResponseDto<DeleteReviewResponseDto>>
    {
        public DeleteReviewRequestDto? DeleteReviewRequestDto { get; set; }
    }
}
