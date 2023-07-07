using Application.Dtos.Common;
using Application.Dtos.Consoles;
using MediatR;

namespace Application.Features.Consoles.Requests.Commands
{
    public class CreateConsoleRequest : IRequest<HttpResponseDto<CreateConsoleResponseDto>>
    {
        public CreateConsoleRequestDto? CreateConsoleRequestDto { get; set; }
    }
}
