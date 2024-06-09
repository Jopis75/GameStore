using Application.Dtos.Common;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Consoles.Requests.Commands
{
    public class DeleteConsoleRequest : IRequest<HttpResponseDto<ConsoleDto>>
    {
        public int? Id { get; set; }
    }
}
