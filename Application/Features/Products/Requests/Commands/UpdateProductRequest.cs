using Application.Dtos.Common;
using Application.Dtos.Products;
using MediatR;

namespace Application.Features.Products.Requests.Commands
{
    public class UpdateProductRequest : IRequest<HttpResponseDto<UpdateProductResponseDto>>
    {
        public UpdateProductRequestDto? UpdateProductRequestDto { get; set; }
    }
}
