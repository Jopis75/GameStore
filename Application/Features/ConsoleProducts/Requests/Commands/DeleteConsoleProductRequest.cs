using Application.Dtos.Common;
using Application.Dtos.ConsoleProducts;
using MediatR;

namespace Application.Features.ConsoleProducts.Requests.Commands
{
    public class DeleteConsoleProductRequest : IRequest<HttpResponseDto<DeleteConsoleProductResponseDto>>
    {
        public DeleteConsoleProductRequestDto? DeleteConsoleProductRequestDto { get; set; }
    }
}
