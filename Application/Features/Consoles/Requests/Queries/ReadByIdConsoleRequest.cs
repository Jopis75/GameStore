using Application.Dtos.Common;
using Application.Dtos.Consoles;
using MediatR;

namespace Application.Features.Consoles.Requests.Queries
{
    public class ReadByIdConsoleRequest : IRequest<HttpResponseDto<ReadByIdConsoleResponseDto>>
    {
        public ReadByIdConsoleRequestDto? ReadByIdConsoleRequestDto { get; set; }
    }
}
