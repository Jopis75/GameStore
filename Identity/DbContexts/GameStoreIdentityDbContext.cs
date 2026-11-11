using GameStore.Application.Models.Identity;
using GameStore.Identity.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Identity.DbContexts
{
    public class GameStoreIdentityDbContext(DbContextOptions<GameStoreIdentityDbContext> dbContextOptions) : IdentityDbContext<ApplicationUser>(dbContextOptions)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}
