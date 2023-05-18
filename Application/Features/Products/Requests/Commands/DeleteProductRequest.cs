using Application.Dtos.Common;
using Application.Dtos.Products;
using MediatR;

namespace Application.Features.Products.Requests.Commands
{
    public class DeleteProductRequest : IRequest<HttpResponseDto<DeleteProductResponseDto>>
    {
        public DeleteProductRequestDto? DeleteProductRequestDto { get; set; }
    }
}
