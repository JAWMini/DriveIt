using Xunit;
using DriveItAPI.Model;
using DriveItAPI.Data;
using DriveItAPI.IntegrationTests.Helpers;
using System;
using System.Linq;

namespace DriveItAPI.IntegrationTests
{
    public class RentalOffersIntegrationTests
    {
        [Fact]
        public void CreateRentalOffer_WithCarShouldUseCarPrices()
        {
            // Arrange
            var context = InMemoryDbContextHelper.GetContext("RentalOffersTestDb");

            var car = new Car
            {
                Id = Guid.NewGuid(),
                Brand = "Ford",
                Model = "Fiesta",
                Year = 2020,
                Available = true,
                City = "Gdansk",
                RentPricePerDay = 80m,
                InsurancePricePerDay = 12m
            };

            // Dodajemy Car do bazy
            context.Cars.Add(car);

            // Tworzymy RentalOffer na podstawie Car
            var offer = new RentalOffer(car);

            // Act
            context.RentOffers.Add(offer);
            context.SaveChanges();

            // Assert
            var offerFromDb = context.RentOffers.FirstOrDefault(o => o.Car.Id == car.Id);
            Assert.NotNull(offerFromDb);
            Assert.Equal(80m, offerFromDb.RentPricePerDay);            // rent price z Car
            Assert.Equal(12m, offerFromDb.InsurancePriccePerDay);      // insurance price z Car
            Assert.Equal(10, offerFromDb.OfferTimeLimit);              // domyślna wartość
        }
    }
}
