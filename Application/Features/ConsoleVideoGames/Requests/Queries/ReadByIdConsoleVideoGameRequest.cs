using Application.Dtos.Common;
using Application.Dtos.ConsoleVideoGames;
using MediatR;

namespace Application.Features.ConsoleVideoGames.Requests.Queries
{
    public class ReadByIdConsoleVideoGameRequest : IRequest<HttpResponseDto<ReadConsoleVideoGameResponseDto>>
    {
        public ReadByIdConsoleVideoGameRequestDto? ReadByIdConsoleVideoGameRequestDto { get; set; }
    }
}
