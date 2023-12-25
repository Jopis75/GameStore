using Application.Dtos.Addresses;
using Application.Dtos.Common;
using Application.Dtos.VideoGames;
using Domain.Entities;

namespace Application.Dtos.Companies
{
    public class ReadAllCompanyResponseDto : ReadAllResponseDto
    {
        public CompanyType CompanyType { get; set; }

        public string EmailAddress { get; set; } = default!;

        public ReadAllAddressResponseDto Headquarter { get; set; } = new();

        public Industry Industry { get; set; }

        public string LogoImageUri { get; set; } = default!;

        public string Name { get; set; } = default!;

        public ReadAllCompanyResponseDto? ParentCompany { get; set; }

        public string PhoneNumber { get; set; } = default!;

        public string TradeName { get; set; } = default!;

        public List<ReadAllVideoGameResponseDto> VideoGames { get; set; } = new();

        public string WebsiteUrl { get; set; } = default!;
    }
}
