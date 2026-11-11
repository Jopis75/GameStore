using GameStore.Application.Dtos.General;
using GameStore.Domain.Dtos;
using MediatR;

namespace GameStore.Application.Features.Consoles.Requests.Commands
{
    public class DeleteConsoleRequest : IRequest<HttpResponseDto<ConsoleDto>>
    {
        public int Id { get; set; }
    }
}
