using DriveIt.Model;

namespace DriveIt.Services
{
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
    }
}