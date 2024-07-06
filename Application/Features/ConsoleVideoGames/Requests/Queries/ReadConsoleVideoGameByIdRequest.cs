using Application.Dtos.General;
using Domain.Dtos;
using MediatR;

namespace Application.Features.ConsoleVideoGames.Requests.Queries
{
    public class ReadConsoleVideoGameByIdRequest : IRequest<HttpResponseDto<ConsoleVideoGameDto>>
    {
        public int? Id { get; set; }
    }
}
