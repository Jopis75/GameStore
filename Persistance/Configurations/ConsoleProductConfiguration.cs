using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations
{
    public class ConsoleProductConfiguration : IEntityTypeConfiguration<ConsoleProduct>
    {
        public void Configure(EntityTypeBuilder<ConsoleProduct> builder)
        {
            builder.HasData(
                new ConsoleProduct
                {
                    ConsoleId = 1,
                    ProductId = 1
                },
                new ConsoleProduct
                {
                    ConsoleId = 1,
                    ProductId = 2
                },
                new ConsoleProduct
                {
                    ConsoleId = 1,
                    ProductId = 3
                }
            );
        }
    }
}
