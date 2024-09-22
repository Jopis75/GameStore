using Application.Dtos.General;
using Domain.Dtos;
using Domain.Entities;
using MediatR;

namespace Application.Features.VideoGames.Requests.Commands
{
    public class CreateVideoGameRequest : IRequest<HttpResponseDto<VideoGameDto>>
    {
        public int DeveloperId { get; set; }

        public string? ImageUri { get; set; }

        public string Name { get; set; } = String.Empty;

        public decimal Price { get; set; }

        public DateTime PurchaseDate { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string? Url { get; set; }

        public string Title { get; set; } = String.Empty!;

        public TimeSpan TotalTimePlayed { get; set; }
    }
}
