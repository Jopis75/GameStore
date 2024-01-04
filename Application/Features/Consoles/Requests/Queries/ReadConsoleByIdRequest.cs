using Application.Dtos.Common;
using Application.Dtos.Consoles;
using MediatR;

namespace Application.Features.Consoles.Requests.Queries
{
    public class ReadConsoleByIdRequest : IRequest<HttpResponseDto<ReadConsoleResponseDto>>
    {
        public ReadByIdRequestDto? ReadByIdRequestDto { get; set; }
    }
}
