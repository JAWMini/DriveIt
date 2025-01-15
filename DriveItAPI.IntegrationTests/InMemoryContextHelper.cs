using Microsoft.EntityFrameworkCore;
using DriveItAPI.Data;
using DriveItAPI.Model;

namespace DriveItAPI.IntegrationTests.Helpers
{
    public static class InMemoryDbContextHelper
    {
        public static CarRentalContext GetContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<CarRentalContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new CarRentalContext(options);
        }
    }
}