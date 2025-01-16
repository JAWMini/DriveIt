using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using DriveItAPI.Model;

namespace DriveItAPI.IntegrationTests
{
    public class CarsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CarsControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetCars_ReturnsListOfAvailableCars()
        {
            // Act
            var response = await _client.GetAsync("/cars");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK, "API powinno zwrócić status 200 OK dla listy samochodów");

            var cars = await response.Content.ReadFromJsonAsync<List<Car>>();
            cars.Should().NotBeNull("API powinno zwrócić listę samochodów");
            cars!.Should().NotBeEmpty("API powinno zwrócić przynajmniej jeden samochód, jeśli są dostępne");

            // Dodatkowa weryfikacja zawartości (opcjonalnie)
            foreach (var car in cars)
            {
                car.Brand.Should().NotBeNullOrEmpty("Samochód powinien mieć określoną markę");
                car.Model.Should().NotBeNullOrEmpty("Samochód powinien mieć określony model");
                car.Year.Should().BeGreaterThan(0, "Rok produkcji powinien być dodatni");
            }
        }
    }
}
