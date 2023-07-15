using Application.Dtos.Addresses;
using Application.Dtos.Common;
using Application.Dtos.Products;
using Domain.Entities;

namespace Application.Dtos.Companies
{
    public class ReadByIdCompanyResponseDto : ReadByIdResponseDto
    {
        public CompanyType? CompanyType { get; set; }

        public string? EmailAddress { get; set; }

        public ReadByIdAddressResponseDto? Headquarters { get; set; } = new();

        public Industry? Industry { get; set; }

        public string? LogoImageUri { get; set; }

        public string? Name { get; set; }

        public ReadByIdCompanyResponseDto? ParentCompany { get; set; } = new();

        public string? PhoneNumber { get; set; }

        public List<ReadByIdProductResponseDto>? Products { get; set; } = new();

        public string? TradeName { get; set; }

        public string? WebsiteUrl { get; set; }
    }
}
