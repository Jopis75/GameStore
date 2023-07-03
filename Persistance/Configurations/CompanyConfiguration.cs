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
                    CompanyType = CompanyType.Division,
                    Name = "PlayStation Studios",
                    TradeName = "PlayStation Studios",
                    Founded = DateTime.Parse("2005-09-14"),
                    HeadquartersId = 1,
                    NumberOfEmployees = 4000,
                    ParentCompanyId = null,
                    WebsiteUrl = "https://www.playstation.com/en-us/corporate/playstation-studios/",
                    LogoImageUri = string.Empty,
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = string.Empty,
                    UpdatedAt = null,
                    DeletedBy = string.Empty,
                    DeletedAt = null
                },
                new Company
                {
                    Id = 2,
                    CompanyType = CompanyType.Subsidiary,
                    Name = "Guerrilla",
                    TradeName = "Guerrilla Games",
                    Founded = DateTime.Parse("2000-01-01"),
                    HeadquartersId = 2,
                    NumberOfEmployees = 360,
                    ParentCompanyId = 1,
                    WebsiteUrl = "https://www.guerrilla-games.com/",
                    LogoImageUri = string.Empty,
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = string.Empty,
                    UpdatedAt = null,
                    DeletedBy = string.Empty,
                    DeletedAt = null
                },
                new Company
                {
                    Id = 3,
                    CompanyType = CompanyType.Subsidiary,
                    Name = "Sucker Punch",
                    TradeName = "Sucker Punch Productions",
                    Founded = DateTime.Parse("1997-10-01"),
                    HeadquartersId = 3,
                    NumberOfEmployees = 160,
                    ParentCompanyId = 1,
                    WebsiteUrl = "https://www.suckerpunch.com/",
                    LogoImageUri = string.Empty,
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = string.Empty,
                    UpdatedAt = null,
                    DeletedBy = string.Empty,
                    DeletedAt = null
                }
            );
        }
    }
}
