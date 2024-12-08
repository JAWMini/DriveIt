using DriveIt.DTOs;
using DriveIt.Model;
using System.Threading.Tasks;
using DriveIt.Data;
using SendGrid.Helpers.Mail;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;

namespace DriveIt.Services
{
    public class RentalService
    {
        private readonly HttpClient _httpClient;
        private readonly CarRentalContext _context;

        public RentalService(CarRentalContext context, HttpClient client)
        {
            _httpClient = client;
            _context = context;       
        }

        public async Task AddRentalAsync(Rental rental)
        {
            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();
        }

        public async Task<Rental> ConfirmRentalAsync(Guid offerId, Guid userId)
        {           
            var RentalDTO = new RentalRequestDto(offerId,userId);

            var response = await _httpClient.PostAsJsonAsync("rentals", RentalDTO);

            return await response.Content.ReadFromJsonAsync<Rental>();
        }

        public async Task<List<Rental>> GetRentalsAsync()
        {
            return await _context.Rentals.ToListAsync();
        }
    }
}
