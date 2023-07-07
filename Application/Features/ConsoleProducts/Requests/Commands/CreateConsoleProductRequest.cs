using Application.Dtos.Common;
using Application.Dtos.ConsoleProducts;
using MediatR;

namespace Application.Features.ConsoleProducts.Requests.Commands
{
    public class CreateConsoleProductRequest : IRequest<HttpResponseDto<CreateConsoleProductResponseDto>>
    {
        public CreateConsoleProductRequestDto? CreateConsoleProductRequestDto { get; set; }
    }
}
