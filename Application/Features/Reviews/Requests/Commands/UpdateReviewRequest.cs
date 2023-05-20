using Application.Dtos.Common;
using Application.Dtos.Reviews;
using MediatR;

namespace Application.Features.Reviews.Requests.Commands
{
    public class UpdateReviewRequest : IRequest<HttpResponseDto<UpdateReviewResponseDto>>
    {
        public UpdateReviewRequestDto? UpdateReviewRequestDto { get; set; }
    }
}
