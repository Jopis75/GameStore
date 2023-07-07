using Application.Dtos.Common;
using Application.Dtos.ConsoleProducts;
using MediatR;

namespace Application.Features.ConsoleProducts.Requests.Commands
{
    public class UpdateConsoleProductRequest : IRequest<HttpResponseDto<UpdateConsoleProductResponseDto>>
    {
        public UpdateConsoleProductRequestDto? UpdateConsoleProductRequestDto { get; set; }
    }
}
