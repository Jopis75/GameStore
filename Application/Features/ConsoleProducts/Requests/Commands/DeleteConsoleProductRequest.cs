using Application.Dtos.ConsoleProducts;
using MediatR;

namespace Application.Features.ConsoleProducts.Requests.Commands
{
    public class DeleteConsoleProductRequest : IRequest<DeleteConsoleProductRequestDto>
    {
        public DeleteConsoleProductRequestDto? DeleteConsoleProductRequestDto { get; set; }
    }
}
