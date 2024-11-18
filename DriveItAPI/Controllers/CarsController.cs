using DriveItAPI.Data;
using DriveItAPI.DTOs;
using DriveItAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DriveItAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly CarRentalContext _db;

        public CarsController(CarRentalContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<CarDto>>> GetCars()
        {
            return await _db.Cars.
                Select(c => new CarDto(c.Brand, c.Model, c.Year, c.Id)).
                ToListAsync();
        }
    }
}
