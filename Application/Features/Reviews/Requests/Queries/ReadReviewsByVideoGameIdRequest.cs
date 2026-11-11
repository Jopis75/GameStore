using GameStore.Application.Dtos.General;
using GameStore.Domain.Dtos;
using MediatR;

namespace GameStore.Application.Features.Reviews.Requests.Queries
{
    public class ReadReviewsByVideoGameIdRequest : IRequest<HttpResponseDto<ReviewDto>>
    {
        public int VideoGameId { get; set; }
    }
}
