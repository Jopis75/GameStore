namespace Domain.Entities
{
    public class Company : EntityBase
    {
        public CompanyType CompanyType { get; set; }

        public string? Description { get; set; }

        public int? Founded { get; set; }

        public virtual Address? HeadOffice { get; set; }

        public int? HeadOfficeId { get; set; }

        public string? LogoImageUri { get; set; }

        public string? Name { get; set; }

        public int? NumberOfEmployees { get; set; }

        public virtual Company? ParentCompany { get; set; }

        public int? ParentCompanyId { get; set; }

        public string? TradeName { get; set; }

        public virtual ICollection<Product> VideoGames { get; set; } = new List<Product>();

        public string? WebsiteUrl { get; set; }
    }
}
