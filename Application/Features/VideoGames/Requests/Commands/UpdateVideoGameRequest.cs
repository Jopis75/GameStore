using Application.Dtos.Common;
using Domain.Dtos;
using MediatR;

namespace Application.Features.VideoGames.Requests.Commands
{
    public class UpdateVideoGameRequest : IRequest<HttpResponseDto<VideoGameDto>>
    {
        public VideoGameDto? VideoGameDto { get; set; }
    }
}
