using Xunit;
using DriveItAPI.Model;
using DriveItAPI.Data;
using DriveItAPI.IntegrationTests.Helpers;
using System;
using System.Linq;

namespace DriveItAPI.IntegrationTests
{
    public class RentalsIntegrationTests
    {
        [Fact]
        public void AddRental_ShouldSaveCarRelationship()
        {
            // Arrange
            var context = InMemoryDbContextHelper.GetContext("RentalsTestDb");

            // Tworzymy Car do powiązania z Rental
            var car = new Car
            {
                Id = Guid.NewGuid(),
                Brand = "Honda",
                Model = "Civic",
                Year = 2021,
                Available = true,
                City = "Krakow",
                RentPricePerDay = 120m,
                InsurancePricePerDay = 10m
            };

            // Dodajemy Car do kontekstu
            context.Cars.Add(car);

            // Tworzymy Rental
            var rental = new Rental
            {
                Id = Guid.NewGuid(),
                Car = car, // Ustawiamy właściwość Car
                UserId = Guid.NewGuid(),
                StartDate = DateTime.Now,
                Status = RentalStatus.Rented,
                Cost = null
            };

            // Act
            context.Rents.Add(rental);
            context.SaveChanges();

            // Assert
            var rentalFromDb = context.Rents.FirstOrDefault(r => r.Car.Id == car.Id);
            Assert.NotNull(rentalFromDb);
            Assert.Equal(RentalStatus.Rented, rentalFromDb.Status);
            Assert.Equal(car.Id, rentalFromDb.Car.Id);
        }
    }
}
