using Application.Dtos.Common;
using Application.Dtos.Reviews;
using MediatR;

namespace Application.Features.Reviews.Requests.Commands
{
    public class CreateReviewRequest : IRequest<HttpResponseDto<CreateReviewResponseDto>>
    {
        public CreateReviewRequestDto? CreateReviewRequestDto { get; set; }
    }
}
