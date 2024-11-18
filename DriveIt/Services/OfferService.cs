using DriveIt.Data;
using DriveIt.Model;
using System.Text;
using System.Text.Json;

namespace DriveIt.Services
{
    public class OfferService
    {
        private readonly CarRentalContext _context;
        private readonly HttpClient _httpClient;

        public OfferService(CarRentalContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        // Metoda pobierająca ofertę na podstawie ID
        public async Task<Offer?> GetOfferByIdAsync(Guid offerId)
        {
            return await _context.Offers.FindAsync(offerId);     
        }

        // TODO
        //// Metoda wysyłająca zapytanie HTTP na podstawie oferty
        public async Task<bool> ConfirmOfferAsync(Offer offer)
        {
            var requestPayload = new
            {
                OfferId = offer.Id,
                RentPrice = offer.RentPrice,
                InsurancePrice = offer.InsurancePrice,
                OfferTimeLimit = offer.OfferTimeLimit,
                IntegratorName = offer.IntegratorName,
                IntegratorUrl = offer.IntegratorUrl
            };

            var content = new StringContent(
                JsonSerializer.Serialize(requestPayload),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(offer.IntegratorUrl, content);

            return response.IsSuccessStatusCode;
        }
    }

}
