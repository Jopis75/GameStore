using GameStore.Application.Dtos.General;
using GameStore.Domain.Dtos;
using MediatR;

namespace GameStore.Application.Features.VideoGames.Requests.Queries
{
    public class ReadMostPlayedVideoGameByConsoleIdRequest : IRequest<HttpResponseDto<VideoGameDto>>
    {
        public int ConsoleId { get; set; }
    }
}
