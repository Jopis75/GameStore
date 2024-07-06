using Application.Dtos.General;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Consoles.Requests.Queries
{
    public class ReadConsoleByIdRequest : IRequest<HttpResponseDto<ConsoleDto>>
    {
        public int? Id { get; set; }
    }
}
