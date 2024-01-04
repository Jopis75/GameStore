using Application.Dtos.Common;
using Application.Dtos.Consoles;
using MediatR;

namespace Application.Features.Consoles.Requests.Queries
{
    public class ReadByIdConsoleRequest : IRequest<HttpResponseDto<ReadConsoleResponseDto>>
    {
        public ReadByIdConsoleRequestDto? ReadByIdConsoleRequestDto { get; set; }
    }
}
