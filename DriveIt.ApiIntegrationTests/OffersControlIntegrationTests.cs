using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using DriveItAPI.DTOs; // Namespace, gdzie masz OfferRequestDto, OfferDto


namespace DriveItAPI.IntegrationTests
{
    // Klasa bazowa, używana przez wszystkie testy integracyjne
    public class IntegrationTestBase : IClassFixture<WebApplicationFactory<Program>>
    {
        protected readonly HttpClient _client;

        public IntegrationTestBase(WebApplicationFactory<Program> factory)
        {
            // Tworzymy klienta HTTP, który będzie wysyłał żądania do uruchomionej aplikacji (API)
            _client = factory.CreateClient();
        }
    }
}

namespace DriveItAPI.IntegrationTests
{
    public class OffersControllerTests : IntegrationTestBase
    {
        public OffersControllerTests(WebApplicationFactory<Program> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task PostOffer_ReturnsCreatedOffer_WhenCarExists()
        {
            // Arrange
            var existingCarId = new Guid("0f5985b2-8255-4d40-e5e4-08dd3338358f");

            var offerRequestDto = new OfferRequestDto(carId: existingCarId, driverLicenseLength: 5, userAge: 30);

            // Act
            // Wysyłamy POST /offers z danymi
            var response = await _client.PostAsJsonAsync("/offers", offerRequestDto);

            // Assert
            // Sprawdź kod odpowiedzi
            response.StatusCode.Should().Be(HttpStatusCode.OK, because: "Kontroler zwraca 200 przy sukcesie");

            // Sprawdź zawartość odpowiedzi
            var offerDto = await response.Content.ReadFromJsonAsync<OfferDto>();
            offerDto.Should().NotBeNull("powinniśmy otrzymać poprawny obiekt");
            offerDto!.Id.Should().NotBeEmpty("nowo utworzona oferta powinna mieć unikalne Id");
            offerDto.RentPrice.Should().BeGreaterThanOrEqualTo(0, "ustawiona cena nie powinna być ujemna");
            offerDto.IntegratorName.Should().NotBeNullOrEmpty("powinniśmy mieć IntegratorName");
        }

        [Fact]
        public async Task PostOffer_ReturnsNoContent_WhenCarDoesNotExist()
        {
            // Arrange
            var nonExistentCarId = Guid.NewGuid(); // Generujemy losowy ID samochodu
            var offerRequestDto = new OfferRequestDto(carId: nonExistentCarId, driverLicenseLength: 5, userAge: 30);

            // Act
            var response = await _client.PostAsJsonAsync("/offers", offerRequestDto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent, "Kontroler zwraca 204 NoContent przy braku Car");
        }
    }
}