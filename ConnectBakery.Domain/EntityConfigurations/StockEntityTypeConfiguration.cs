using ConnectBakery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConnectBakery.Domain.EntityConfigurations
{
    public class StockEntityTypeConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable("stocks");

            builder.HasOne(x => x.Product).WithOne().HasForeignKey<Stock>(c => c.ProductId);
        }
    }

}
