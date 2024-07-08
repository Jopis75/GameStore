using Application.Dtos.General;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Reviews.Requests.Commands
{
    public class DeleteReviewRequest : IRequest<HttpResponseDto<ReviewDto>>
    {
        public int Id { get; set; }
    }
}
