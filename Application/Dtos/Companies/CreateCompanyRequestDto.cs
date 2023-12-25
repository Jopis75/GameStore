using Application.Dtos.Common;
using Domain.Entities;

namespace Application.Dtos.Companies
{
    public class CreateCompanyRequestDto : CreateRequestDto
    {
        public CompanyType CompanyType { get; set; }

        public string EmailAddress { get; set; } = default!;

        public int HeadquartersId { get; set; }

        public Industry Industry { get; set; }

        public string LogoImageUri { get; set; } = default!;

        public string Name { get; set; } = default!;

        public int? ParentCompanyId { get; set; }

        public string PhoneNumber { get; set; } = default!;

        public string TradeName { get; set; } = default!;

        public string WebsiteUrl { get; set; } = default!;
    }
}
