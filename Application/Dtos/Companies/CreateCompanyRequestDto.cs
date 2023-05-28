using Application.Dtos.Common.Interfaces;
using Domain.Entities;

namespace Application.Dtos.Companies
{
    public class CreateCompanyRequestDto : ICreateRequestDto
    {
        public CompanyType? CompanyType { get; set; }

        public string? Description { get; set; }

        public int? Founded { get; set; }

        public int? HeadOfficeId { get; set; }

        public string? LogoImageUri { get; set; }

        public string? Name { get; set; }

        public int? NumberOfEmployees { get; set; }

        public int? ParentCompanyId { get; set; }

        public string? TradeName { get; set; }

        public string? WebsiteUrl { get; set; }
    }
}
