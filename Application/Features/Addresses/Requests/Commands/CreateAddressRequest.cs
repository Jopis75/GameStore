using Application.Dtos.Addresses;
using Application.Dtos.Common;
using MediatR;

namespace Application.Features.Addresses.Requests.Commands
{
    public class CreateAddressRequest : IRequest<HttpResponseDto<CreateAddressResponseDto>>
    {
        public CreateAddressRequestDto? CreateAddressRequestDto { get; set; }
    }
}
