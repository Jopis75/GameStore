using Application.Dtos.General;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Reviews.Requests.Queries
{
    public class ReadReviewAllRequest : IRequest<HttpResponseDto<List<ReviewDto>>>
    {
    }
}
