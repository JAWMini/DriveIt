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

            RentOffer rentOffer = new(car);
            _db.RentOffers.Add(rentOffer);

            OfferDto rentOfferDto = new(rentOffer.Id, rentOffer.RentPricePerDay, rentOffer.InsurancePriccePerDay, rentOffer.OfferTimeLimit);
            return rentOfferDto;
        }
    }
}

