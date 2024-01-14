using Application.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> entityTypeBuilder)
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();

            entityTypeBuilder.HasData(
                new ApplicationUser
                {
                    Id = "3bd83c97-8e27-42f4-ba1f-327b3cb143ed",
                    FirstName = "Johan",
                    LastName = "Steinrud",
                    Email = "johan.steinrud@gmail.com",
                    NormalizedEmail = "JOHAN.STEINRUD@GMAIL.COM",
                    EmailConfirmed = true,
                    UserName = "johan.steinrud@gmail.com",
                    NormalizedUserName = "JOHAN.STEINRUD@GMAIL.COM",
                    PasswordHash = passwordHasher.HashPassword(new ApplicationUser(), "~ZhCp4%*QK~gqJ+}")
                }
            );
        }
    }
}
