using Application.Dtos.Common;
using Application.Dtos.ConsoleProducts;
using MediatR;

namespace Application.Features.ConsoleProducts.Requests.Queries
{
    public class ReadAllConsoleProductRequest : IRequest<HttpResponseDto<ReadAllConsoleProductResponseDto>>
    {
    }
}
