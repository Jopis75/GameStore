using Application.Dtos.ConsoleProducts;
using MediatR;

namespace Application.Features.ConsoleProducts.Requests.Commands
{
    public class CreateConsoleProductRequest : IRequest<CreateConsoleProductRequestDto>
    {
        public CreateConsoleProductRequestDto? CreateConsoleProductRequestDto { get; set; }
    }
}
