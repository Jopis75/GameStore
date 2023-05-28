using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> entityTypeBuilder)
        {
            entityTypeBuilder.HasData(
                new IdentityUserRole<string>
                {
                    UserId = "3bd83c97-8e27-42f4-ba1f-327b3cb143ed",
                    RoleId = "91836a37-a716-463a-91b1-7c1d1608972b"
                }
            );
        }
    }
}
