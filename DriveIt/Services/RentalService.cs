using DriveIt.Data;
using DriveIt.DTOs;
using DriveIt.Model;
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

        public async Task<Rental?> ConfirmRentalAsync(Guid offerId, Guid userId)
        {
            var rentalRequestDto = new RentalRequestDto(offerId, userId);

            var response = await _httpClient.PostAsJsonAsync("rentals", rentalRequestDto);

            if(!response.IsSuccessStatusCode)
            {
                return null;
            }

            RentalDto? rentalDto = await response.Content.ReadFromJsonAsync<RentalDto>();

            if (rentalDto is null)
                return null;

            var carDto = rentalDto.CarDto;
            var car = new Car(carDto.Brand, carDto.Model, carDto.Year, carDto.Id, carDto.City);

            var rental = new Rental(rentalDto.Id, car, rentalDto.UserId, rentalDto.StartDate);

            return rental;

            //return await response.Content.ReadFromJsonAsync<Rental>();
        }

        public async Task<Rental?> GetRentalAsync(Guid id)
        {
            return await _context.Rentals.FindAsync(id);
        }

        public async Task<List<Rental>> GetActiveRentalsByUserIdAsync(Guid userId)
        {
            return await _context.Rentals.Where(r => r.UserId == userId && r.Status == RentalStatus.Rented).ToListAsync();
        }

        public async Task<List<Rental>> GetAcceptanceRequestedRentalByUserIdAsync(Guid userId)
        {
            return await _context.Rentals.Where(r => r.UserId == userId && r.Status == RentalStatus.AcceptanceRequested).ToListAsync();
        }

        public async Task FinishRental(Rental rental)
        {
            rental.Status = RentalStatus.AcceptanceRequested;
            rental.ReturnDate = DateTime.Now;
            await _context.SaveChangesAsync();
        }
    }
}
