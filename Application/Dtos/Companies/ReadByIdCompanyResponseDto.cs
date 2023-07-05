using Application.Dtos.Addresses;
using Application.Dtos.Common.Interfaces;
using Application.Dtos.Products;
using Domain.Entities;

namespace Application.Dtos.Companies
{
    public class ReadByIdCompanyResponseDto : IReadByIdResponseDto
    {
        public CompanyType? CompanyType { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }

        public string? EmailAddress { get; set; }

        public ReadByIdAddressResponseDto? Headquarters { get; set; } = new();

        public int Id { get; set; }

        public Industry? Industry { get; set; }

        public string? LogoImageUri { get; set; }

        public string? Name { get; set; }

        public ReadByIdCompanyResponseDto? ParentCompany { get; set; } = new();

        public string? PhoneNumber { get; set; }

        public List<ReadByIdProductResponseDto>? Products { get; set; } = new();

        public string? TradeName { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public string? WebsiteUrl { get; set; }
    }
}
