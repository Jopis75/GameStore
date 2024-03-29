using Application.Dtos.Common;
using Application.Dtos.VideoGames;
using MediatR;

namespace Application.Features.VideoGames.Requests.Queries
{
    public class ReadMostPlayedVideoGameByConsoleIdRequest : IRequest<HttpResponseDto<ReadVideoGameResponseDto>>
    {
        public ReadMostPlayedVideoGameByConsoleIdRequestDto? ReadMostPlayedVideoGameByConsoleIdRequestDto { get; set; }
    }
}
