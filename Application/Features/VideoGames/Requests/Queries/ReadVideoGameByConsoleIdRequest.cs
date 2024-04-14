using Application.Dtos.Common;
using Application.Dtos.VideoGames;
using MediatR;

namespace Application.Features.VideoGames.Requests.Queries
{
    public class ReadVideoGameByConsoleIdRequest : IRequest<HttpResponseDto<List<ReadVideoGameResponseDto>>>
    {
        public ReadVideoGameByConsoleIdRequestDto? ReadVideoGameByConsoleIdRequestDto { get; set; }
    }
}
