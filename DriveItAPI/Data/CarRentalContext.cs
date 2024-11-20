using DriveItAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace DriveItAPI.Data
{
    public class CarRentalContext : DbContext
    {
        public CarRentalContext(DbContextOptions<CarRentalContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<RentalOffer> RentOffers { get; set; }
        public DbSet<Rental> Rents { get; set; }
    }
}
