using GameStore.Application.Dtos.General;
using GameStore.Domain.Dtos;
using MediatR;

namespace GameStore.Application.Features.Reviews.Requests.Queries
{
    public class ReadReviewByIdRequest : IRequest<HttpResponseDto<ReviewDto>>
    {
        public int Id { get; set; }
    }
}
