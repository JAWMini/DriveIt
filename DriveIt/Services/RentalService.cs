using DriveIt.Data;
using DriveIt.DTOs;
using DriveIt.EmailSenders;
using DriveIt.Model;
<<<<<<< HEAD
using System.Threading.Tasks;
using DriveIt.Data;
using SendGrid.Helpers.Mail;
using System.Net.Http;
=======
using Microsoft.AspNetCore.Components;
>>>>>>> b113696 (Add email sending when renting and returning a car. Add cost calculating and displaying.)
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DriveIt.Services
{
    public class RentalService
    {
        private readonly HttpClient _httpClient;
        private readonly CarRentalContext _context;
        private readonly IGeneralEmailSender _emailSender;

        public RentalService(CarRentalContext context, HttpClient client, IGeneralEmailSender emailSender)
        {
            _httpClient = client;
            _context = context;
            _emailSender = emailSender;
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

        public async Task<List<Rental>> GetRentalsByUserIdAsync(Guid userId)
        {
            return await _context.Rentals.Where(r => r.UserId == userId).ToListAsync();
        }
        public async Task<List<Rental>> GetRentalsAsync()
        {
            return await _context.Rentals.ToListAsync();
        }


        public async Task<bool> AcceptRentalAsync(Rental rental)
        {
            var response = await _httpClient.PostAsJsonAsync($"rentals/accept/{rental.Id}", rental.Id);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            rental.Status = RentalStatus.Accepted;
            rental.AcceptedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> FinishRental(Rental rental)
        {
            var response = await _httpClient.PostAsJsonAsync($"rentals/return/{rental.Id}", rental.Id);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            decimal? cost = await response.Content.ReadFromJsonAsync<decimal>();
            if(cost is null)
            {
                return false;
            }

            rental.Status = RentalStatus.AcceptanceRequested;
            rental.ReturnDate = DateTime.Now;
            rental.Cost = cost;
            await _context.SaveChangesAsync();

            return true;
        }




        public async Task SendRentalConfirmationEmailAsync(string userEmail, Rental rental)
        {
            var emailBody = $"Rozpoczęto wypożyczenie samochodu {rental.Car.Brand} {rental.Car.Model}. Data rozpoczęcia: {rental.StartDate}.";
            await _emailSender.SendEmailAsync(userEmail, "Rozpoczęcie wypożyczenia samochodu", emailBody);
        }

        public async Task SendRentalFinishEmailAsync(string userEmail, Rental rental)
        {
            var emailBody = $"Zakończono wypożyczenie samochodu {rental.Car.Brand} {rental.Car.Model}. Data zakończenia: {rental.ReturnDate}. Koszt: {rental.Cost} zł.";
            await _emailSender.SendEmailAsync(userEmail, "Zakończenie wypożyczenia samochodu", emailBody);
        }
    }
}
