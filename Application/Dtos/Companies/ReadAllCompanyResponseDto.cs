using Application.Dtos.Addresses;
using Application.Dtos.Common;
using Application.Dtos.Products;
using Domain.Entities;

namespace Application.Dtos.Companies
{
    public class ReadAllCompanyResponseDto : ReadAllResponseDto
    {
        public CompanyType? CompanyType { get; set; }

        public string? EmailAddress { get; set; }

        public ReadAllAddressResponseDto? Headquarters { get; set; } = new();

        public Industry? Industry { get; set; }

        public string? LogoImageUri { get; set; }

        public string? Name { get; set; }

        public ReadAllCompanyResponseDto? ParentCompany { get; set; } = new();

        public string? PhoneNumber { get; set; }

        public List<ReadAllProductResponseDto>? Products { get; set; } = new();

        public string? TradeName { get; set; }

        public string? WebsiteUrl { get; set; }
    }
}
