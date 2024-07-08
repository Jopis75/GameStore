using Application.Dtos.General;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Reviews.Requests.Queries
{
    public class ReadReviewsByVideoGameIdRequest : IRequest<HttpResponseDto<List<ReviewDto>>>
    {
        public int VideoGameId { get; set; }
    }
}
