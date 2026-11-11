using GameStore.Application.Dtos.General;
using GameStore.Domain.Dtos;
using MediatR;

namespace GameStore.Application.Features.ConsoleVideoGames.Requests.Commands
{
    public class CreateConsoleVideoGameRequest : IRequest<HttpResponseDto<ConsoleVideoGameDto>>
    {
        public int ConsoleId { get; set; }

        public int VideoGameId { get; set; }
    }
}
