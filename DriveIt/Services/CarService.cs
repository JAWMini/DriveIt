using DriveIt.DTOs;
using DriveIt.Model;
using System.Threading.Tasks;

namespace DriveIt.Services
{

    // TODO: Dodanie pozostałych integratorów
    public class CarService
    {
        private readonly HttpClient _httpClient;

        public CarService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Metoda pobierająca samochody z API
        public async Task<List<Car>> GetCarsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Car>>("cars");
            }
            catch (HttpRequestException ex)
            {
                // Obsługa błędu sieciowego
                Console.WriteLine($"Błąd sieci: {ex.Message}");
                return [];
            }
            catch (Exception ex)
            {
                // Inna obsługa błędów
                Console.WriteLine($"Błąd: {ex.Message}");
                return [];
            }
        }

        //public async Task<List<Offer>> GetOffersAsync(OfferRequest offerRequest)
        //{
        //    string url = "offers";
        //    OfferRequestDto offerRequestDto = new(offerRequest.CarId, offerRequest.DrivingLicenseLength, offerRequest.UserAge);
        //    HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, offerRequestDto);

        //    response.EnsureSuccessStatusCode();
        //    string responseContent = await response.Content.ReadAsStringAsync();

        //    OfferDto? offerDto = await response.Content.ReadFromJsonAsync<OfferDto>();

        //    if(offerDto is null)
        //        return [];

        //    Offer offer = new(offerDto.Id, offerDto.RentPrice, offerDto.InsurancePrice, offerDto.OfferTimeLimit, offerDto.IntegratorName, offerDto.IntegratorUrl);
        //    //Offer offer = new(offerDto);

        //    return [offer];
        //}


    }
}