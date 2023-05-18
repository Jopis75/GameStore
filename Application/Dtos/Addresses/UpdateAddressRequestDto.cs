using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Addresses
{
    public class UpdateAddressRequestDto : IUpdateRequestDto
    {
        public string? City { get; set; }

        public int? CompanyId { get; set; }

        public string? Country { get; set; }

        public int Id { get; set; }

        public string? PostalCode { get; set; }

        public string? State { get; set; }

        public string? StreetAddress { get; set; }
    }
}
