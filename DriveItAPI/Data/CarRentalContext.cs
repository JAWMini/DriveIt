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
        public DbSet<RentOffer> RentOffers { get; set; }
        //public DbSet<Model.Rent> Rents { get; set; }
    }
}
