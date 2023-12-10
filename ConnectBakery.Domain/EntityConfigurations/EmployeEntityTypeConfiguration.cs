using ConnectBakery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConnectBakery.Domain.EntityConfigurations
{
    public class EmployeEntityTypeConfiguration : IEntityTypeConfiguration<Employe>
    {
        public void Configure(EntityTypeBuilder<Employe> builder)
        {
            //table name
            builder.ToTable("employes");

            builder.Property(c => c.FirstName).HasMaxLength(30).IsRequired();
            builder.Property(c => c.LastName).HasMaxLength(30).IsRequired();

            builder.HasIndex(x => x.PhoneNumber).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();

            builder.HasOne(x => x.User).WithOne().HasForeignKey<Employe>(c => c.UserId); 
        }
    }

}
