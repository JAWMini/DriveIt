using DriveItAPI.Data;
using DriveItAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DriveItAPI.Controllers
{
    //[Route("api/[controller]")]
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
        public async Task<ActionResult<List<Car>>> GetCars()
        {
            return await _db.Cars.ToListAsync();
        }
    }
}
