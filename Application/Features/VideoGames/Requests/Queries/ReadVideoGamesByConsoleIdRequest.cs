using Application.Dtos.General;
using Domain.Dtos;
using MediatR;

namespace Application.Features.VideoGames.Requests.Queries
{
    public class ReadVideoGamesByConsoleIdRequest : IRequest<HttpResponseDto<List<VideoGameDto>>>
    {
        public int ConsoleId { get; set; }
    }
}
