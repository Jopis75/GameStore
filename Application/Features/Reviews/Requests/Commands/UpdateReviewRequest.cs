using Application.Dtos.Common;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Reviews.Requests.Commands
{
    public class UpdateReviewRequest : IRequest<HttpResponseDto<ReviewDto>>
    {
        public ReviewDto? ReviewDto { get; set; }
    }
}
