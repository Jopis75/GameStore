using Domain.Entities;
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
                    EmailAddress = "johan.steinrud@gmail.com",
                    PhoneNumber = "46702651007",
                    WebsiteUrl = "https://sonyinteractive.com/en/",
                    LogoImageUri = String.Empty,
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = String.Empty,
                    UpdatedAt = null,
                    DeletedBy = String.Empty,
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
                    EmailAddress = "johan.steinrud@gmail.com",
                    PhoneNumber = "46702651007",
                    WebsiteUrl = "https://www.playstation.com/en-us/corporate/playstation-studios/",
                    LogoImageUri = String.Empty,
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = String.Empty,
                    UpdatedAt = null,
                    DeletedBy = String.Empty,
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
                    EmailAddress = "johan.steinrud@gmail.com",
                    PhoneNumber = "46702651007",
                    WebsiteUrl = "https://www.guerrilla-games.com/",
                    LogoImageUri = String.Empty,
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = String.Empty,
                    UpdatedAt = null,
                    DeletedBy = String.Empty,
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
                    EmailAddress = "johan.steinrud@gmail.com",
                    PhoneNumber = "46702651007",
                    WebsiteUrl = "https://www.suckerpunch.com/",
                    LogoImageUri = String.Empty,
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = String.Empty,
                    UpdatedAt = null,
                    DeletedBy = String.Empty,
                    DeletedAt = null
                }
            );
        }
    }
}
