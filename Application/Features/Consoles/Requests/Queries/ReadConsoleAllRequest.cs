using GameStore.Application.Dtos.General;
using GameStore.Domain.Dtos;
using MediatR;

namespace GameStore.Application.Features.Consoles.Requests.Queries
{
    public class ReadConsoleAllRequest : IRequest<HttpResponseDto<ConsoleDto>>
    {
    }
}
