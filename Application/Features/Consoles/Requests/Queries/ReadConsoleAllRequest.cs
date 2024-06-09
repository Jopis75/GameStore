using Application.Dtos.Common;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Consoles.Requests.Queries
{
    public class ReadConsoleAllRequest : IRequest<HttpResponseDto<List<ConsoleDto>>>
    {
    }
}
