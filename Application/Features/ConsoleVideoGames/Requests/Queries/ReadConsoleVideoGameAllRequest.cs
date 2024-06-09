using Application.Dtos.Common;
using Domain.Dtos;
using MediatR;

namespace Application.Features.ConsoleVideoGames.Requests.Queries
{
    public class ReadConsoleVideoGameAllRequest : IRequest<HttpResponseDto<List<ConsoleVideoGameDto>>>
    {
    }
}
