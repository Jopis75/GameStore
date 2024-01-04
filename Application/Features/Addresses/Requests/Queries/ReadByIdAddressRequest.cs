using Application.Dtos.Addresses;
using Application.Dtos.Common;
using MediatR;

namespace Application.Features.Addresses.Requests.Queries
{
    public class ReadByIdAddressRequest : IRequest<HttpResponseDto<ReadAddressResponseDto>>
    {
        public ReadByIdAddressRequestDto? ReadByIdAddressRequestDto { get; set; }
    }
}
