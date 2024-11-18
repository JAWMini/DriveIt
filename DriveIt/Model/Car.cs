namespace DriveIt.Model
{
    public class Car
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public Guid Id { get; set; }
        public string City { get; set; }

        public Car(string brand, string model, int year, Guid id, string city)
        {
            Brand = brand;
            Model = model;
            Year = year;
            Id = id;
            City = city;
        }
    }
}
