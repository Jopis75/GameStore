using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasData(
                new Address
                {
                    Id = 1,
                    StreetAddress = "2207 Bridgepointe Pkwy",
                    PostalCode = "94404",
                    City = "San Mateo",
                    State = "California",
                    Country = "USA",
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = null,
                    UpdatedAt = null,
                    DeletedBy = null,
                    DeletedAt = null
                },
                new Address
                {
                    Id = 2,
                    StreetAddress = "2207 Bridgepointe Pkwy",
                    PostalCode = "94404",
                    City = "San Mateo",
                    State = "California",
                    Country = "United States",
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = null,
                    UpdatedAt = null,
                    DeletedBy = null,
                    DeletedAt = null
                },
                new Address
                {
                    Id = 3,
                    StreetAddress = "Nieuwezijds Voorburgwal 225",
                    PostalCode = "1012 RL",
                    City = "Amsterdam",
                    State = String.Empty,
                    Country = "The Netherlands",
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = null,
                    UpdatedAt = null,
                    DeletedBy = null,
                    DeletedAt = null
                },
                new Address
                {
                    Id = 4,
                    StreetAddress = "500 108th Avenue North East Suite 2600",
                    PostalCode = "98004",
                    City = "Bellevue",
                    State = "Washington",
                    Country = "United States",
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = null,
                    UpdatedAt = null,
                    DeletedBy = null,
                    DeletedAt = null
                },
                new Address
                {
                    //3-1-3 Uchihirano-machi, Chuo-ku, Osaka
                    //540-0037, Japan
                    //TEL : 81(6)6920-3600
                    //FAX: 81(6)6920-5100
                    Id = 5,
                    StreetAddress = "3-1-3 Uchihirano-machi",
                    PostalCode = "540-0037",
                    City = "Osaka",
                    State = "Chuo-ku",
                    Country = "Japan",
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
