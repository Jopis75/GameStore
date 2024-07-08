using Application.Dtos.General;
using Domain.Dtos;
using MediatR;

namespace Application.Features.VideoGames.Requests.Queries
{
    public class ReadMostPlayedVideoGameByConsoleIdRequest : IRequest<HttpResponseDto<VideoGameDto>>
    {
        public int ConsoleId { get; set; }
    }
}
