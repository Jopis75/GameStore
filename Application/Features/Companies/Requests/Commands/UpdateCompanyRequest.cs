using Application.Dtos.General;
using Domain.Dtos;
using Domain.Enums;
using MediatR;

namespace Application.Features.Companies.Requests.Commands
{
    public class UpdateCompanyRequest : IRequest<HttpResponseDto<CompanyDto>>
    {
        public CompanyType CompanyType { get; set; }

        public string EmailAddress { get; set; } = String.Empty;

        public int HeadquarterId { get; set; }

        public int Id { get; set; }

        public Industry Industry { get; set; }

        public string? LogoImageUri { get; set; }

        public string Name { get; set; } = String.Empty;

        public int? ParentCompanyId { get; set; }

        public string PhoneNumber { get; set; } = String.Empty;

        public string TradeName { get; set; } = String.Empty;

        public string? WebsiteUrl { get; set; }
    }
}
