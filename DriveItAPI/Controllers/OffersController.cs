using DriveItAPI.Data;
using DriveItAPI.DTOs;
using DriveItAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace DriveItAPI.Controllers
{
    [ApiController]
    [Route("offers")]
    public class OffersController : ControllerBase
    {
        private readonly CarRentalContext _db;
        public OffersController(CarRentalContext db)
        {
            _db = db;
        }


        [HttpPost]
        public async Task<ActionResult<OfferDto>?> PostOffer(OfferRequestDto offerRequestDto)
        {
            Car? car = await _db.Cars.FindAsync(offerRequestDto.CarId);

            if (car is null)
                return null;

            RentalOffer rentOffer = new(car);
            _db.RentOffers.Add(rentOffer);
            _db.SaveChanges();
            int ageTax = offerRequestDto.UserAge switch
            {
                < 21 => 20,
                < 25 => 10,
                < 65 => 0,
                _ => 20
            };
            var rentPricePerDay = rentOffer.RentPricePerDay - offerRequestDto.DriverLicenseLength * 2 + ageTax;
            OfferDto rentOfferDto = new(rentOffer.Id,rentPricePerDay , rentOffer.InsurancePriccePerDay, rentOffer.OfferTimeLimit);
            return rentOfferDto;
        }
    }
}

