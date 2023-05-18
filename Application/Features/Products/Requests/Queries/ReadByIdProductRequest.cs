using Application.Dtos.Common;
using Application.Dtos.Products;
using MediatR;

namespace Application.Features.Products.Requests.Queries
{
    public class ReadByIdProductRequest : IRequest<HttpResponseDto<ReadByIdProductResponseDto>>
    {
        public ReadByIdProductRequestDto? ReadByIdProductRequestDto { get; set; }
    }
}
