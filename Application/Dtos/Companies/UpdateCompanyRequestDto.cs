using Application.Dtos.Common;
using Domain.Entities;
using System.ComponentModel;

namespace Application.Dtos.Companies
{
    public class UpdateCompanyRequestDto : UpdateRequestDto
    {
        public CompanyType CompanyType { get; set; }

        public string EmailAddress { get; set; } = default!;

        public int HeadquarterId { get; set; }

        public Industry Industry { get; set; }

        public string? LogoImageUri { get; set; }

        public string Name { get; set; } = default!;

        public int? ParentCompanyId { get; set; }

        public string PhoneNumber { get; set; } = default!;

        public string TradeName { get; set; } = default!;

        public string? WebsiteUrl { get; set; }
    }
}
