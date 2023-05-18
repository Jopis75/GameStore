using Application.Dtos.Addresses;
using Application.Dtos.Common;
using MediatR;

namespace Application.Features.Addresses.Requests.Commands
{
    public class DeleteAddressRequest : IRequest<HttpResponseDto<DeleteAddressResponseDto>>
    {
        public DeleteAddressRequestDto? DeleteAddressRequestDto { get; set; }
    }
}
