using Application.Dtos.General;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Reviews.Requests.Commands
{
    public class UpdateReviewRequest : IRequest<HttpResponseDto<ReviewDto>>
    {
        public int? ConsoleId { get; set; }

        // Grade between 0 and 100.
        public int Grade { get; set; }

        public DateTime ReviewDate { get; set; }

        public string ReviewText { get; set; } = String.Empty;

        public int? VideoGameId { get; set; }
    }
}
