using Domain.Enums;

namespace Domain.Entities
{
    public class Company : EntityBase
    {
        public CompanyType CompanyType { get; set; }

        public virtual ICollection<Console> Consoles { get; set; } = new List<Console>();

        public virtual ICollection<VideoGame> DevelopedVideoGames { get; set; } = new List<VideoGame>();

        public string EmailAddress { get; set; } = default!;

        public virtual Address Headquarter { get; set; } = default!;

        public int HeadquarterId { get; set; }

        public Industry Industry { get; set; }

        public string? LogoImageUri { get; set; }

        public string Name { get; set; } = default!;

        public virtual Company? ParentCompany { get; set; }

        public int? ParentCompanyId { get; set; }

        public string PhoneNumber { get; set; } = default!;

        public virtual ICollection<VideoGame> PublishedVideoGames { get; set; } = new List<VideoGame>();

        public string TradeName { get; set; } = default!;

        public string? WebsiteUrl { get; set; }
    }
}
