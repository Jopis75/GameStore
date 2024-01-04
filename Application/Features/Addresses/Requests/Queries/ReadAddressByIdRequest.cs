using Application.Dtos.Addresses;
using Application.Dtos.Common;
using MediatR;

namespace Application.Features.Addresses.Requests.Queries
{
    public class ReadAddressByIdRequest : IRequest<HttpResponseDto<ReadAddressResponseDto>>
    {
        public ReadByIdRequestDto? ReadByIdRequestDto { get; set; }
    }
}
