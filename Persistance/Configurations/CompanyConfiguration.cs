﻿using Domain.Entities;
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
                    HeadquartersId = 1,
                    ParentCompanyId = null,
                    WebsiteUrl = "https://sonyinteractive.com/en/",
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
                    CompanyType = CompanyType.Division,
                    Name = "PlayStation Studios",
                    TradeName = "PlayStation Studios",
                    HeadquartersId = 2,
                    ParentCompanyId = 2,
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
                    Id = 3,
                    CompanyType = CompanyType.Subsidiary,
                    Name = "Guerrilla",
                    TradeName = "Guerrilla Games",
                    HeadquartersId = 3,
                    ParentCompanyId = 2,
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
                    Id = 4,
                    CompanyType = CompanyType.Subsidiary,
                    Name = "Sucker Punch",
                    TradeName = "Sucker Punch Productions",
                    HeadquartersId = 4,
                    ParentCompanyId = 2,
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
