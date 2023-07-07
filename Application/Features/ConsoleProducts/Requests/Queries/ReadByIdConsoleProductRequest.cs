using Application.Dtos.Common;
using Application.Dtos.ConsoleProducts;
using MediatR;

namespace Application.Features.ConsoleProducts.Requests.Queries
{
    public class ReadByIdConsoleProductRequest : IRequest<HttpResponseDto<ReadByIdConsoleProductResponseDto>>
    {
        public ReadByIdConsoleProductRequestDto? ReadByIdConsoleProductRequestDto { get; set; }
    }
}
