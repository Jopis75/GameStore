using Application.Dtos.General;
using Domain.Dtos;
using MediatR;

namespace Application.Features.VideoGames.Requests.Commands
{
    public class CreateVideoGameRequest : IRequest<HttpResponseDto<VideoGameDto>>
    {
        public VideoGameDto VideoGameDto { get; set; } = new();
    }
}
