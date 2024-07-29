using Domain.Enums;

namespace Domain.Filters
{
    public class CompanyFilter : FilterBase
    {
        public CompanyType? CompanyType { get; set; }

        public string? EmailAddress { get; set; }

        public int? HeadquarterId { get; set; }

        public Industry? Industry { get; set; }

        public string? LogoImageUri { get; set; }

        public string? Name { get; set; }

        public int? ParentCompanyId { get; set; }

        public string? PhoneNumber { get; set; }

        public string? TradeName { get; set; }

        public string? WebsiteUrl { get; set; }
    }
}
