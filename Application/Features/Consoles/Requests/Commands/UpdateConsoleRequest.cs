using Application.Dtos.Common;
using Application.Dtos.Consoles;
using MediatR;

namespace Application.Features.Consoles.Requests.Commands
{
    public class UpdateConsoleRequest : IRequest<HttpResponseDto<UpdateConsoleResponseDto>>
    {
        public UpdateConsoleRequestDto? UpdateConsoleRequestDto { get; set; }
    }
}
