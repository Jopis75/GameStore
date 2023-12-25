using Application.Dtos.Common;
using Application.Dtos.ConsoleVideoGames;
using MediatR;

namespace Application.Features.ConsoleVideoGames.Requests.Queries
{
    public class ReadAllConsoleVideoGameRequest : IRequest<HttpResponseDto<ReadAllConsoleVideoGameResponseDto>>
    {
    }
}
