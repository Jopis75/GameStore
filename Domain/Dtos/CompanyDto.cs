namespace Domain.Dtos
{
    public class CompanyDto : DtoBase
    {
        public CompanyTypeEnum CompanyType { get; set; } = new();

        public List<ConsoleDto> Consoles { get; set; } = new();

        public string EmailAddress { get; set; } = default!;

        public AddressDto Headquarter { get; set; } = new();

        public IndustryEnum Industry { get; set; } = new();

        public string? LogoImageUri { get; set; }

        public string Name { get; set; } = default!;

        public CompanyDto? ParentCompany { get; set; } = new();

        public string PhoneNumber { get; set; } = default!;

        public string TradeName { get; set; } = default!;

        public List<VideoGameDto> VideoGames { get; set; } = new();

        public string? WebsiteUrl { get; set; }
    }
}
