using DriveIt.Model;

namespace DriveIt.DTOs
{
    public record class CarDto
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public Guid Id { get; set; }
        public string City { get; set; }


        public CarDto() { }

        public CarDto(string brand, string model, int year, Guid id, string city)
        {
            Brand = brand;
            Model = model;
            Year = year;
            Id = id;
            City = city;
        }

        public CarDto(Car car) : this(car.Brand, car.Model, car.Year, car.Id, car.City)
        {
        }
    }
}
