using DriveIt.Data;
using DriveIt.DTOs;
using DriveIt.EmailSenders;
using DriveIt.Model;
using System.Text;
using System.Text.Json;

namespace DriveIt.Services
{
    public class OfferService
    {
        private readonly CarRentalContext _context;
        private readonly HttpClient _httpClient;
        private readonly TokenService _tokenService;
        private readonly IGeneralEmailSender _emailSender;

        public OfferService(CarRentalContext context, HttpClient httpClient, TokenService tokenService, IGeneralEmailSender emailSender)
        {
            _context = context;
            _httpClient = httpClient;
            _tokenService = tokenService;
            _emailSender = emailSender;
        }

        // Metoda pobierająca ofertę na podstawie ID
        public async Task<Offer?> GetLocalOfferByIdAsync(Guid offerId)
        {
            return await _context.Offers.FindAsync(offerId);
        }
        public async Task<List<Offer>> GetOffersAsync(OfferRequest offerRequest)
        {
            string url = "offers";
            OfferRequestDto offerRequestDto = new(offerRequest.CarId, offerRequest.DrivingLicenseLength, offerRequest.UserAge);
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, offerRequestDto);

            response.EnsureSuccessStatusCode();
            string responseContent = await response.Content.ReadAsStringAsync();

            OfferDto? offerDto = await response.Content.ReadFromJsonAsync<OfferDto>();

            if (offerDto is null)
                return [];

            Offer offer = new(offerDto.Id, offerDto.RentPrice, offerDto.InsurancePrice, offerDto.OfferTimeLimit, offerDto.IntegratorName, offerDto.IntegratorUrl);
            //Offer offer = new(offerDto);

            return [offer];
        }

        public async Task SendRentalOfferEmailAsync(string userEmail, Offer offer)
        {
            var token = _tokenService.GenerateToken(offer.Id, offer.OfferTimeLimit);
            var callbackUrl = $"https://localhost:7100/potwierdzenie-wypożyczenia?token={Uri.EscapeDataString(token)}";

            var emailBody = $"Kliknij w ten link, aby zaakceptować ofertę: <a href=\"{callbackUrl}\">Akceptuj ofertę</a>. Link jest ważny przez {offer.OfferTimeLimit} minut.";

            await _emailSender.SendEmailAsync(userEmail, "Oferta wypożyczenia samochodu", emailBody);
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
