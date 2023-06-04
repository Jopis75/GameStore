using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistance.DbContexts
{
    public class GameStoreDbContext : AuditableDbContext
    {
        public DbSet<Address> Addresses { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public GameStoreDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Address.
            modelBuilder
                .Entity<Address>()
                .ToTable("Address");

            // Company.
            modelBuilder
                .Entity<Company>()
                .ToTable("Company");
            modelBuilder
                .Entity<Company>()
                .HasMany(company => company.Products)
                .WithOne(Product => Product.VideoGameDeveloper)
                .HasForeignKey(product => product.VideoGameDeveloperId);

            // Product.
            modelBuilder
                .Entity<Product>()
                .ToTable("Product");
            modelBuilder
                .Entity<Product>()
                .HasOne(product => product.VideoGameDeveloper)
                .WithMany(company => company.Products)
                .HasForeignKey(product => product.VideoGameDeveloperId);
            modelBuilder
                .Entity<Product>()
                .HasOne(product => product.Review)
                .WithOne(review => review.VideoGame)
                .HasForeignKey<Product>(product => product.ReviewId);

            // Review.
            modelBuilder
                .Entity<Review>()
                .ToTable("Review");
            modelBuilder
                .Entity<Review>()
                .HasOne(review => review.VideoGame)
                .WithOne(product => product.Review)
                .HasForeignKey<Review>(review => review.VideoGameId);

        }
    }
}
