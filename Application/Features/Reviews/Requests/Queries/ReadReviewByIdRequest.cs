using Application.Dtos.Common;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Reviews.Requests.Queries
{
    public class ReadReviewByIdRequest : IRequest<HttpResponseDto<ReviewDto>>
    {
        public int? Id { get; set; }
    }
}
