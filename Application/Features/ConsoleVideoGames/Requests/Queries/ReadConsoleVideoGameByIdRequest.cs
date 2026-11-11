using GameStore.Application.Dtos.General;
using GameStore.Domain.Dtos;
using MediatR;

namespace GameStore.Application.Features.ConsoleVideoGames.Requests.Queries
{
    public class ReadConsoleVideoGameByIdRequest : IRequest<HttpResponseDto<ConsoleVideoGameDto>>
    {
        public int Id { get; set; }
    }
}
