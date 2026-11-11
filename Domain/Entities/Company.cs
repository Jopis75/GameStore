using GameStore.Domain.Enums;

namespace GameStore.Domain.Entities
{
    public class Company : EntityBase
    {
        public CompanyType CompanyType { get; set; }

        public virtual ICollection<Console> Consoles { get; set; } = [];

        public virtual ICollection<VideoGame> DevelopedVideoGames { get; set; } = [];

        public string EmailAddress { get; set; } = default!;

        public virtual Address Headquarter { get; set; } = default!;

        public int HeadquarterId { get; set; }

        public Industry Industry { get; set; }

        public string? LogoImageUri { get; set; }

        public string Name { get; set; } = default!;

        public virtual Company? ParentCompany { get; set; }

        public int? ParentCompanyId { get; set; }

        public string PhoneNumber { get; set; } = default!;

        public virtual ICollection<VideoGame> PublishedVideoGames { get; set; } = [];

        public string TradeName { get; set; } = default!;

        public string? WebsiteUrl { get; set; }
    }
}
