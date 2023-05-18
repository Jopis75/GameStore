using Application.Dtos.Common;
using Application.Dtos.Products;
using MediatR;

namespace Application.Features.Products.Requests.Queries
{
    public class ReadAllProductRequest : IRequest<HttpResponseDto<ReadAllProductResponseDto>>
    {
    }
}
