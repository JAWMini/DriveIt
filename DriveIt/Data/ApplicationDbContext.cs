using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DriveIt.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<ApplicationUser>("ApplicationUser")
                .HasValue<Customer>("Customer");

            // Konfiguracja dla klasy Customer
            builder.Entity<Customer>(entity =>
            {
                entity.Property(c => c.DateOfBirth).IsRequired();
                entity.Property(c => c.DriverLicenseYear).IsRequired();
            });
        }
    }
}
