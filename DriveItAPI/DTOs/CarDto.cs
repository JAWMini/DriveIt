using DriveItAPI.Model;

namespace DriveItAPI.DTOs
{
    public record class CarDto(string Brand, string Model, int Year, Guid Id, string City)
    {
        public CarDto(Car car) : this(car.Brand, car.Model, car.Year, car.Id, car.City)
        {
        }
    }
}
