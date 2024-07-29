using Domain.Enums;

namespace Domain.Dtos
{
    public class CompanyDto : DtoBase
    {
        public CompanyType CompanyType { get; set; }

        public List<ConsoleDto> Consoles { get; set; } = new();

        public string EmailAddress { get; set; } = default!;

        public AddressDto Headquarter { get; set; } = new();

        public int HeadquarterId { get; set; }

        public Industry Industry { get; set; }

        public string? LogoImageUri { get; set; }

        public string Name { get; set; } = default!;

        public CompanyDto? ParentCompany { get; set; } = new();

        public int? ParentCompanyId { get; set; }

        public string PhoneNumber { get; set; } = default!;

        public string TradeName { get; set; } = default!;

        public List<VideoGameDto> VideoGames { get; set; } = new();

        public string? WebsiteUrl { get; set; }
    }
}
