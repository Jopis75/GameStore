using Application.Dtos.Common;
using Application.Dtos.Consoles;
using MediatR;

namespace Application.Features.Consoles.Requests.Commands
{
    public class DeleteConsoleRequest : IRequest<HttpResponseDto<DeleteConsoleResponseDto>>
    {
        public DeleteConsoleRequestDto? DeleteConsoleRequestDto { get; set; }
    }
}
