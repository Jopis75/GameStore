using Application.Dtos.Common;
using Domain.Dtos;
using MediatR;

namespace Application.Features.ConsoleVideoGames.Requests.Commands
{
    public class UpdateConsoleVideoGameRequest : IRequest<HttpResponseDto<ConsoleVideoGameDto>>
    {
        public ConsoleVideoGameDto? ConsoleVideoGameDto { get; set; }
    }
}
