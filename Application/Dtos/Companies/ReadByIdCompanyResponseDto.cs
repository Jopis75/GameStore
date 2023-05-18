using Application.Dtos.Addresses;
using Application.Dtos.Common.Interfaces;
using Application.Dtos.Products;

namespace Application.Dtos.Companies
{
    public class ReadByIdCompanyResponseDto : IReadByIdResponseDto
    {
        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }

        public string? Description { get; set; }

        public int Founded { get; set; }

        public ReadByIdAddressResponseDto HeadOffice { get; set; } = new();

        public int Id { get; set; }

        public string? LogoImageUri { get; set; }

        public string? Name { get; set; }

        public int NumberOfEmployees { get; set; }

        public ReadByIdCompanyResponseDto ParentCompany { get; set; } = new();

        public List<ReadByIdProductResponseDto> Products { get; set; } = new();

        public string? TradeName { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public string? WebsiteUrl { get; set; }
    }
}
