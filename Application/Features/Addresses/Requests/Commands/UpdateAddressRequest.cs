using Application.Dtos.General;
using MediatR;

namespace Application.Features.Addresses.Requests.Commands
{
    public class UpdateAddressRequest : IRequest<HttpResponseDto<UpdateAddressRequest>>
    {
        public string City { get; set; } = String.Empty;

        public string Country { get; set; } = String.Empty;

        public int Id { get; set; }

        public string PostalCode { get; set; } = String.Empty;

        public string State { get; set; } = String.Empty;

        public string StreetAddress { get; set; } = String.Empty;
    }
}
