using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasData(
                new Company
                {
                    Id = 1,
                    CompanyType = CompanyType.Subsidiary,
                    Name = "Sony Interactive Entertainment",
                    TradeName = "Sony Interactive Entertainment",
                    HeadquarterId = 1,
                    ParentCompanyId = null,
                    EmailAddress = String.Empty,
                    PhoneNumber = String.Empty,
                    WebsiteUrl = "https://sonyinteractive.com/en/",
                    LogoImageUri = null,
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = null,
                    UpdatedAt = null,
                    DeletedBy = null,
                    DeletedAt = null
                },
                new Company
                {
                    Id = 2,
                    CompanyType = CompanyType.Division,
                    Name = "PlayStation Studios",
                    TradeName = "PlayStation Studios",
                    HeadquarterId = 2,
                    ParentCompanyId = 2,
                    EmailAddress = String.Empty,
                    PhoneNumber = String.Empty,
                    WebsiteUrl = "https://www.playstation.com/en-us/corporate/playstation-studios/",
                    LogoImageUri = null,
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = null,
                    UpdatedAt = null,
                    DeletedBy = null,
                    DeletedAt = null
                },
                new Company
                {
                    Id = 3,
                    CompanyType = CompanyType.Subsidiary,
                    Name = "Guerrilla",
                    TradeName = "Guerrilla Games",
                    HeadquarterId = 3,
                    ParentCompanyId = 2,
                    EmailAddress = String.Empty,
                    PhoneNumber = String.Empty,
                    WebsiteUrl = "https://www.guerrilla-games.com/",
                    LogoImageUri = null,
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = null,
                    UpdatedAt = null,
                    DeletedBy = null,
                    DeletedAt = null
                },
                new Company
                {
                    Id = 4,
                    CompanyType = CompanyType.Subsidiary,
                    Name = "Sucker Punch",
                    TradeName = "Sucker Punch Productions",
                    HeadquarterId = 4,
                    ParentCompanyId = 2,
                    EmailAddress = String.Empty,
                    PhoneNumber = String.Empty,
                    WebsiteUrl = "https://www.suckerpunch.com/",
                    LogoImageUri = null,
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = null,
                    UpdatedAt = null,
                    DeletedBy = null,
                    DeletedAt = null
                }
            );
        }
    }
}
