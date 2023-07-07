using Application.Dtos.ConsoleProducts;
using MediatR;

namespace Application.Features.ConsoleProducts.Requests.Queries
{
    public class ReadByIdConsoleProductRequest : IRequest<ReadByIdConsoleProductRequestDto>
    {
        public ReadByIdConsoleProductRequestDto? ReadByIdConsoleProductRequestDto { get; set; }
    }
}
