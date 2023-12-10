using ConnectBakery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConnectBakery.Domain.EntityConfigurations
{
    public class ClientEntityTypeConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("clients");

          
            builder.Property(c => c.FirstName).HasMaxLength(30).IsRequired();
            builder.Property(c => c.LastName).HasMaxLength(30).IsRequired();

            builder.HasIndex(x => x.PhoneNumber).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();

            builder.HasOne(x => x.User).WithOne().HasForeignKey<Client>(c => c.UserId); 

        }
    }

}
