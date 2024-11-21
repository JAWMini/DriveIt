using DriveItAPI.Data;
using DriveItAPI.DTOs;
using DriveItAPI.Model;
using Microsoft.AspNetCore.Mvc;

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


        [HttpPost]
        public async Task<ActionResult<RentalDto>?> PostRental(RentalRequestDto rentRequestDto)
        {
            var offer = await _db.RentOffers.FindAsync(rentRequestDto.OfferId);
            if (offer is null)
                return NotFound();

            var car = await _db.Cars.FindAsync(offer.Car.Id);
            if (car is null)
                return NotFound();

            if (!car.Available)
                return BadRequest("Car is not available");

            var rental = new Rental(Guid.NewGuid(), car, rentRequestDto.UserId, DateTime.Now);

            _db.Rents.Add(rental);
            await _db.SaveChangesAsync();

            var carDto = new CarDto(car);

            return new RentalDto(rental.Id, carDto, rental.UserId, rental.StartDate);

            //return CreatedAtAction("PostRental", new { id = offer.Id }, new RentalDto(offer.Id, carDto, rental.UserId, rental.StartDate));
        }
    }
}
