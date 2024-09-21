using Application.Dtos.General;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Addresses.Requests.Commands
{
    public class UpdateAddressRequest : IRequest<HttpResponseDto<AddressDto>>
    {
        public string City { get; set; } = String.Empty;

        public string Country { get; set; } = String.Empty;

        public string PostalCode { get; set; } = String.Empty;

        public string State { get; set; } = String.Empty;

        public string StreetAddress { get; set; } = String.Empty;
    }
}
