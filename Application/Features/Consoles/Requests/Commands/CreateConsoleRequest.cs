using Application.Dtos.General;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Consoles.Requests.Commands
{
    public class CreateConsoleRequest : IRequest<HttpResponseDto<ConsoleDto>>
    {
        public ConsoleDto ConsoleDto { get; set; } = new();
    }
}
