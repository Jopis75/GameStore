using GameStore.Application.Dtos.General;
using GameStore.Domain.Dtos;
using MediatR;

namespace GameStore.Application.Features.Reviews.Requests.Queries
{
    public class ReadReviewAllRequest : IRequest<HttpResponseDto<ReviewDto>>
    {
    }
}
