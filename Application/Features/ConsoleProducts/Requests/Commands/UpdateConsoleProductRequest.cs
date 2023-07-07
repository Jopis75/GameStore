using Application.Dtos.ConsoleProducts;
using MediatR;

namespace Application.Features.ConsoleProducts.Requests.Commands
{
    public class UpdateConsoleProductRequest : IRequest<UpdateConsoleProductRequestDto>
    {
        public UpdateConsoleProductRequestDto? UpdateConsoleProductRequestDto { get; set; }
    }
}
