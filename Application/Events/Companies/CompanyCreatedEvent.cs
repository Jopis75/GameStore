using Domain.Enums;

namespace Application.Events.Companies
{
    public class CompanyCreatedEvent() : EventBase(nameof(CompanyCreatedEvent))
    {
        public CompanyType CompanyType { get; set; }

        //public List<ConsoleDto> Consoles { get; set; } = [];

        //public List<VideoGameDto> DevelopedVideoGames { get; set; } = [];

        public string EmailAddress { get; set; } = default!;

        //public AddressDto Headquarter { get; set; } = default!;

        public int HeadquarterId { get; set; }

        public Industry Industry { get; set; }

        public string? LogoImageUri { get; set; }

        public string Name { get; set; } = default!;

        //public CompanyDto? ParentCompany { get; set; }

        public int? ParentCompanyId { get; set; }

        public string PhoneNumber { get; set; } = default!;

        //public List<VideoGameDto> PublishedVideoGames { get; set; } = [];

        public string TradeName { get; set; } = default!;

        public string? WebsiteUrl { get; set; }
    }
}
