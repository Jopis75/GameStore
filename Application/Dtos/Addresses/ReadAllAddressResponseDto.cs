using Application.Dtos.Common.Interfaces;
using Application.Dtos.Companies;

namespace Application.Dtos.Addresses
{
    public class ReadAllAddressResponseDto : IReadAllResponseDto
    {
        public string? City { get; set; }

        public ReadAllCompanyResponseDto? Company { get; set; } = new();

        public string? Country { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }

        public int Id { get; set; }

        public string? PostalCode { get; set; }

        public string? State { get; set; }

        public string? StreetAddress { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
