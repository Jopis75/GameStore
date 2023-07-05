namespace Domain.Entities
{
    public class Company : EntityBase
    {
        public CompanyType? CompanyType { get; set; }

        public virtual ICollection<Console>? Consoles { get; set; } = new List<Console>();

        public string? EmailAddress { get; set; }

        public virtual Address? Headquarters { get; set; }

        public int? HeadquartersId { get; set; }

        public Industry? Industry { get; set; }

        public string? LogoImageUri { get; set; }

        public string? Name { get; set; }

        public virtual Company? ParentCompany { get; set; }

        public int? ParentCompanyId { get; set; }

        public string? PhoneNumber { get; set; }

        public virtual ICollection<Product>? Products { get; set; } = new List<Product>();

        public string? TradeName { get; set; }

        public string? WebsiteUrl { get; set; }
    }
}
