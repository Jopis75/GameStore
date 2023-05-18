using Application.Dtos.Addresses;
using Application.Dtos.Common;
using MediatR;

namespace Application.Features.Addresses.Requests.Queries
{
    public class ReadByIdAddressRequest : IRequest<HttpResponseDto<ReadByIdAddressResponseDto>>
    {
        public ReadByIdAddressRequestDto? ReadByIdAddressRequestDto { get; set; }
    }
}
