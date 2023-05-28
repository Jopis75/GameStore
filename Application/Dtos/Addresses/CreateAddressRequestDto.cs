using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Addresses
{
    public class CreateAddressRequestDto : ICreateRequestDto
    {
        public string? City { get; set; }

        public string? Country { get; set; }

        public string? PostalCode { get; set; }

        public string? State { get; set; }

        public string? StreetAddress { get; set; }
    }
}
