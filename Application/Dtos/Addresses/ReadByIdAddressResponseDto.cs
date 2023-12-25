using Application.Dtos.Common;

namespace Application.Dtos.Addresses
{
    public class ReadByIdAddressResponseDto : ReadByIdResponseDto
    {
        public string City { get; set; } = default!;

        public string Country { get; set; } = default!;

        public string PostalCode { get; set; } = default!;

        public string State { get; set; } = default!;

        public string StreetAddress { get; set; } = default!;
    }
}
