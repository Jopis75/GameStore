using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistance.DbContexts
{
    public abstract class AuditableDbContext : DbContext
    {
        public AuditableDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions) { }

        public virtual async Task<int> SaveChangesAsync(string userName = "System")
        {
            var entries = base.ChangeTracker.Entries<EntityBase>()
                .Where(entry => entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.State == EntityState.Deleted);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy = userName;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = userName;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.Entity.DeletedAt = DateTime.UtcNow;
                    entry.Entity.DeletedBy = userName;
                }
            }

            return await base.SaveChangesAsync();
        }
    }
}
