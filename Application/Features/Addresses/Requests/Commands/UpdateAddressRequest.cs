using Application.Dtos.Addresses;
using Application.Dtos.Common;
using MediatR;

namespace Application.Features.Addresses.Requests.Commands
{
    public class UpdateAddressRequest : IRequest<HttpResponseDto<UpdateAddressResponseDto>>
    {
        public UpdateAddressRequestDto? UpdateAddressRequestDto { get; set; }
    }
}
