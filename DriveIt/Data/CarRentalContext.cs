using DriveIt.Model;
using Microsoft.EntityFrameworkCore;

namespace DriveIt.Data
{
    public class CarRentalContext : DbContext
    {
        public CarRentalContext(DbContextOptions<CarRentalContext> options) : base(options)
        {
        }

        public DbSet<Offer> Offers { get; set; }
        public DbSet<Rental> Rentals { get; set; }

    }
}
