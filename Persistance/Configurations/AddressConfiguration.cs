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
                    Country = "United States",
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = string.Empty,
                    UpdatedAt = null,
                    DeletedBy = string.Empty,
                    DeletedAt = null
                },
                new Address
                {
                    Id = 2,
                    StreetAddress = "Nieuwezijds Voorburgwal 225",
                    PostalCode = "1012 RL",
                    City = "Amsterdam",
                    Country = "The Netherlands",
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = string.Empty,
                    UpdatedAt = null,
                    DeletedBy = string.Empty,
                    DeletedAt = null
                },
                new Address
                {
                    Id = 3,
                    StreetAddress = "500 108th Avenue North East Suite 2600",
                    PostalCode = "98004",
                    City = "Bellevue",
                    State = "WA",
                    Country = "United States",
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
