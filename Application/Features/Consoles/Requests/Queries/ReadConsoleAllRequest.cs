using Application.Dtos.Common;
using Application.Dtos.Consoles;
using MediatR;

namespace Application.Features.Consoles.Requests.Queries
{
    public class ReadConsoleAllRequest : IRequest<HttpResponseDto<List<ReadConsoleResponseDto>>>
    {
    }
}
