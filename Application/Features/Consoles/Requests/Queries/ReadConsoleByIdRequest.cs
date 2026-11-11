using GameStore.Application.Dtos.General;
using GameStore.Domain.Dtos;
using MediatR;

namespace GameStore.Application.Features.Consoles.Requests.Queries
{
    public class ReadConsoleByIdRequest : IRequest<HttpResponseDto<ConsoleDto>>
    {
        public int Id { get; set; }
    }
}
