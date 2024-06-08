using Application.Dtos.Common;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Addresses.Requests.Queries
{
    public class ReadAddressAllRequest : IRequest<HttpResponseDto<List<AddressDto>>>
    {
    }
}
