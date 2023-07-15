using Application.Dtos.Common.Interfaces;
using Domain.Entities;

namespace Application.Dtos.Companies
{
    public class CreateCompanyRequestDto : CreateRequestDto
    {
        public CompanyType? CompanyType { get; set; }

        public string? EmailAddress { get; set; }

        public int? HeadquartersId { get; set; }

        public Industry? Industry { get; set; }

        public string? LogoImageUri { get; set; }

        public string? Name { get; set; }

        public int? ParentCompanyId { get; set; }

        public string? PhoneNumber { get; set; }

        public string? TradeName { get; set; }

        public string? WebsiteUrl { get; set; }
    }
}
