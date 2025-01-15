using Xunit;
using DriveItAPI.Model;
using DriveItAPI.Data;
using DriveItAPI.IntegrationTests.Helpers;
using System;
using System.Linq;

namespace DriveItAPI.IntegrationTests
{
    public class CarsIntegrationTests
    {
        [Fact]
        public void AddCar_ShouldSaveToDatabase()
        {
            // Arrange
            var context = InMemoryDbContextHelper.GetContext("CarsTestDb");
            var carId = Guid.NewGuid();
            var car = new Car(
                brand: "Toyota",
                model: "Corolla",
                year: 2022,
                id: carId,
                available: true,
                city: "Warsaw",
                rentPricePerDay: 100m,
                insurancePricePerDay: 15m
            );

            // Act
            context.Cars.Add(car);
            context.SaveChanges();

            // Assert
            var carFromDb = context.Cars.FirstOrDefault(c => c.Id == carId);
            Assert.NotNull(carFromDb);
            Assert.Equal("Toyota", carFromDb.Brand);
            Assert.Equal(2022, carFromDb.Year);
            Assert.True(carFromDb.Available);
        }
    }
}
