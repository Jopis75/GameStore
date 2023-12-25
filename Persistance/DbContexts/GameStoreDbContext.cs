﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.Configurations;
using Console = Domain.Entities.Console;

namespace Persistance.DbContexts
{
    public class GameStoreDbContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<ConsoleVideoGame> ConsoleProducts { get; set; }

        public DbSet<Console> Consoles { get; set; }

        public DbSet<VideoGame> Products { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public GameStoreDbContext(DbContextOptions<GameStoreDbContext> dbContextOptions) 
            : base(dbContextOptions) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Address.
            modelBuilder
                .Entity<Address>()
                .ToTable("Address");
            modelBuilder
                .Entity<Address>()
                .HasKey(address => address.Id);

            // Company.
            modelBuilder
                .Entity<Company>()
                .ToTable("Company");
            modelBuilder
                .Entity<Company>()
                .HasKey(company => company.Id);
            modelBuilder
                .Entity<Company>()
                .HasMany(company => company.VideoGames)
                .WithOne(Product => Product.Developer)
                .HasForeignKey(product => product.DeveloperId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder
                .Entity<Company>()
                .HasOne(company => company.Headquarter)
                .WithOne(address => address.Company)
                .HasForeignKey<Company>(company => company.HeadquarterId)
                .OnDelete(DeleteBehavior.SetNull);

            // Console.
            modelBuilder
                .Entity<Console>()
                .ToTable("Console");
            modelBuilder
                .Entity<Console>()
                .HasKey(console => console.Id);
            modelBuilder
                .Entity<Console>()
                .HasOne(console => console.Developer)
                .WithMany(company => company.Consoles)
                .HasForeignKey(product => product.DeveloperId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder
                .Entity<Console>()
                .HasOne(console => console.Review)
                .WithOne(review => review.Console)
                .HasForeignKey<Console>(product => product.ReviewId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder
                .Entity<Console>()
                .Property(console => console.Price)
                .HasPrecision(18, 2);

            // Product.
            modelBuilder
                .Entity<VideoGame>()
                .ToTable("Product");
            modelBuilder
                .Entity<VideoGame>()
                .HasKey(product => product.Id);
            modelBuilder
                .Entity<VideoGame>()
                .HasOne(product => product.Developer)
                .WithMany(company => company.VideoGames)
                .HasForeignKey(product => product.DeveloperId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder
                .Entity<VideoGame>()
                .HasOne(product => product.Review)
                .WithOne(review => review.VideoGame)
                .HasForeignKey<VideoGame>(product => product.ReviewId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder
                .Entity<VideoGame>()
                .Property(product => product.Price)
                .HasPrecision(18, 2);

            // ConsoleProduct.
            modelBuilder
                .Entity<ConsoleVideoGame>()
                .ToTable("ConsoleProduct");
            modelBuilder.Entity<ConsoleVideoGame>()
                .HasKey(consoleProduct => new { consoleProduct.ConsoleId, consoleProduct.VideoGameId });
            modelBuilder.Entity<ConsoleVideoGame>()
                .HasOne(consoleProduct => consoleProduct.Console)
                .WithMany(console => console.ConsoleVideoGames)
                .HasForeignKey(consoleProduct => consoleProduct.ConsoleId);
            //.OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<ConsoleVideoGame>()
                .HasOne(consoleProduct => consoleProduct.VideoGame)
                .WithMany(product => product.ConsoleVideoGames)
                .HasForeignKey(consoleProduct => consoleProduct.VideoGameId);
                //.OnDelete(DeleteBehavior.SetNull);

            // Review.
            modelBuilder
                .Entity<Review>()
                .ToTable("Review");
            modelBuilder
                .Entity<Review>()
                .HasKey(review => review.Id);
            modelBuilder
                .Entity<Review>()
                .HasOne(review => review.VideoGame)
                .WithOne(product => product.Review)
                .HasForeignKey<Review>(review => review.VideoGameId)
                .OnDelete(DeleteBehavior.SetNull);

            // Configurations.
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new ConsoleConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ConsoleProductConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        }

        public virtual async Task<int> SaveChangesAsync(string userName = "System")
        {
            var entries = base.ChangeTracker.Entries<EntityBase>()
                .Where(entry => entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.State == EntityState.Deleted);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now;
                    entry.Entity.CreatedBy = userName;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.Now;
                    entry.Entity.UpdatedBy = userName;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.Entity.DeletedAt = DateTime.Now;
                    entry.Entity.DeletedBy = userName;
                }
            }

            return await base.SaveChangesAsync();
        }
    }
}
