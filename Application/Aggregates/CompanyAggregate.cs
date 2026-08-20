using Application.Events.Companies;
using Domain.Enums;

namespace Application.Aggregates
{
    public class CompanyAggregate : AggregateRoot
    {
        public bool Active { get; set; }

        public CompanyAggregate()
        {
        }
        
        public CompanyAggregate(string name, string tradeName, CompanyType companyType, Industry industry, int? parentCompanyId, int headquarterId, string? logoImageUri, string emailAddress, string phoneNumber, string? websiteUrl)
        {
            CreateCompany(name, tradeName, companyType, industry, parentCompanyId, headquarterId, logoImageUri, emailAddress, phoneNumber, websiteUrl);
        }

        public void Apply(CompanyCreatedEvent companyCreatedEvent)
        {
            Id = companyCreatedEvent.Id;
            Active = true;
        }

        public void Apply(CompanyDeletedEvent companyDeletedEvent)
        {
            Id = companyDeletedEvent.Id;
            Active = false;
        }

        public void Apply(CompanyUpdatedEvent companyUpdatedEvent)
        {
            Id = companyUpdatedEvent.Id;
        }

        private void CreateCompany(string name, string tradeName, CompanyType companyType, Industry industry, int? parentCompanyId, int headquarterId, string? logoImageUri, string emailAddress, string phoneNumber, string? websiteUrl)
        {
            var companyCreatedEvent = new CompanyCreatedEvent
            {
                Name = name,
                TradeName = tradeName,
                CompanyType = companyType,
                Industry = industry,
                ParentCompanyId = parentCompanyId,
                HeadquarterId = headquarterId,
                LogoImageUri = logoImageUri,
                EmailAddress = emailAddress,
                PhoneNumber = phoneNumber,
                WebsiteUrl = websiteUrl
            };

            RaiseEvent(companyCreatedEvent);
        }

        private void DeleteCompany()
        {
            if (Active == false)
            {
                throw new InvalidOperationException($"Unable to delete the inactive company with Id {Id}.");
            }

            var companyDeletedEvent = new CompanyDeletedEvent
            {
                Id = Id
            };

            RaiseEvent(companyDeletedEvent);
        }

        private void UpdateCompany(string name, string tradeName, CompanyType companyType, Industry industry, int? parentCompanyId, int headquarterId, string? logoImageUri, string emailAddress, string phoneNumber, string? websiteUrl)
        {
            if (Active == false)
            {
                throw new InvalidOperationException($"Unable to delete the inactive company with Id {Id}.");
            }

            var companyUpdatedEvent = new CompanyUpdatedEvent
            {
                Id = Id,
                Name = name,
                TradeName = tradeName,
                CompanyType = companyType,
                Industry = industry,
                ParentCompanyId = parentCompanyId,
                HeadquarterId = headquarterId,
                LogoImageUri = logoImageUri,
                EmailAddress = emailAddress,
                PhoneNumber = phoneNumber,
                WebsiteUrl = websiteUrl
            };

            RaiseEvent(companyUpdatedEvent);
        }
    }
}
