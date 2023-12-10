using ConnectBakery.Domain.Entities;
using ConnectBakery.Domain.EntityConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConnectBakery.DAL
{
    public class ConnectBakeryDbContext : IdentityDbContext<User>
    {
        public ConnectBakeryDbContext(DbContextOptions<ConnectBakeryDbContext> options) : base(options)
        {


        }
       
        //public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Employe> Employes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //modelBuilder.Entity<User>()
            //     .ToTable("KlogerUsers");

            modelBuilder.Entity<IdentityRole>()
                .ToTable("roles");

            modelBuilder.Entity<IdentityUserClaim<string>>()
                .ToTable("userclaims");

            modelBuilder.Entity<IdentityUserRole<string>>()
                .ToTable("userroles");

            modelBuilder.Entity<IdentityUserLogin<string>>()
                .ToTable("userlogins");

            modelBuilder.Entity<IdentityRoleClaim<string>>()
                .ToTable("roleclaims");

            modelBuilder.Entity<IdentityUserToken<string>>()
                .ToTable("usertokens");


            // Ajout de la configuration pour les entités
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ClientEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StockEntityTypeConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            

        }
    }
}
