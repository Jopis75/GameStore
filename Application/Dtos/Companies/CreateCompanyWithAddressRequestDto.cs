using Application.Dtos.Common;
using Domain.Entities;

namespace Application.Dtos.Companies
{
    public class CreateCompanyWithAddressRequestDto : CreateRequestDto
    {
        public CompanyType CompanyType { get; set; }

        public string EmailAddress { get; set; } = default!;

        public Industry Industry { get; set; }

        public string LogoImageUri { get; set; } = default!;

        public string Name { get; set; } = default!;

        public int? ParentCompanyId { get; set; }

        public string PhoneNumber { get; set; } = default!;

        public string TradeName { get; set; } = default!;

        public string WebsiteUrl { get; set; } = default!;

        public string City { get; set; } = default!;

        public string Country { get; set; } = default!;

        public string PostalCode { get; set; } = default!;

        public string State { get; set; } = default!;

        public string StreetAddress { get; set; } = default!;
    }
}
