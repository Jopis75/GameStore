using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> entityTypeBuilder)
        {
            entityTypeBuilder.HasData(
                new IdentityRole
                {
                    Id = "91836a37-a716-463a-91b1-7c1d1608972b",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                },
                new IdentityRole
                {
                    Id = "8a777e31-b642-43a1-be81-f0db12c34ec6",
                    Name = "User",
                    NormalizedName = "USER"
                }
            );
        }
    }
}
