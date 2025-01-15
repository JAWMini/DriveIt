using DriveItAPI.Data;
using DriveItAPI.DTOs;
using DriveItAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DriveItAPI.Controllers
{
    [ApiController]
    [Route("rentals")]
    public class RentalsController : ControllerBase
    {
        private readonly CarRentalContext _db;
        public RentalsController(CarRentalContext db)
        {
            _db = db;
        }


        [HttpPost("return/{id}")]
        public async Task<decimal?> PostRentalReturn(Guid id)
        {
            var rental = await _db.Rents.Include(o => o.Car).FirstOrDefaultAsync(o => o.Id == id);
            if (rental is null)
                return null;

            rental.Status = RentalStatus.AcceptanceRequested;
            rental.ReturnDate = DateTime.Now;

            var days = (rental.ReturnDate - rental.StartDate).Value.Days;
            rental.Cost = days * (rental.Car.RentPricePerDay + rental.Car.InsurancePricePerDay);

            await _db.SaveChangesAsync();

            return rental.Cost;
        }

        [HttpPost("/accept/{id}")]
        public async Task PostRental(Guid id)
        {
            var rental = await _db.Rents.FindAsync(id);

            if (rental is null)
                return;

            rental.Status = RentalStatus.Accepted;
            rental.AcceptedDate = DateTime.Now;
            rental.Car.Available = true;

            await _db.SaveChangesAsync();
        }


        [HttpPost]
        public async Task<ActionResult<RentalDto>?> PostRental(RentalRequestDto rentRequestDto)
        {
            //var offer = await _db.RentOffers.FindAsync(rentRequestDto.OfferId);
            var offer = await _db.RentOffers.Include(o => o.Car).FirstOrDefaultAsync(o => o.Id == rentRequestDto.OfferId);
            if (offer is null)
                return NotFound();

            //var car = await _db.Cars.FindAsync(offer.Car.Id);
            //if (car is null)
            //    return NotFound();

            var car = offer.Car;

            if (!car.Available)
                return BadRequest("Car is not available");

            car.Available = false;
            var rental = new Rental(Guid.NewGuid(), car, rentRequestDto.UserId, DateTime.Now);

            _db.Rents.Add(rental);
            await _db.SaveChangesAsync();

            var carDto = new CarDto(car);

            return new RentalDto(rental.Id, carDto, rental.UserId, rental.StartDate);

            //return CreatedAtAction("PostRental", new { id = offer.Id }, new RentalDto(offer.Id, carDto, rental.UserId, rental.StartDate));
        }
    }
}
