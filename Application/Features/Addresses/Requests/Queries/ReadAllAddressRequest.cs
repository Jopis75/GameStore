using Application.Dtos.Addresses;
using Application.Dtos.Common;
using MediatR;

namespace Application.Features.Addresses.Requests.Queries
{
    public class ReadAllAddressRequest : IRequest<HttpResponseDto<ReadAllAddressResponseDto>>
    {
    }
}
