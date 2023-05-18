using Application.Dtos.Common;
using Application.Dtos.Products;
using MediatR;

namespace Application.Features.Products.Requests.Commands
{
    public class CreateProductRequest : IRequest<HttpResponseDto<CreateProductResponseDto>>
    {
        public CreateProductRequestDto? CreateProductRequestDto { get; set; }
    }
}
