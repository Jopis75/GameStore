using GameStore.Application.Dtos.General;
using GameStore.Domain.Dtos;
using MediatR;

namespace GameStore.Application.Features.Reviews.Requests.Commands
{
    public class DeleteReviewRequest : IRequest<HttpResponseDto<ReviewDto>>
    {
        public int Id { get; set; }
    }
}
