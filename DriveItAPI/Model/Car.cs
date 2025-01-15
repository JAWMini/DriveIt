namespace DriveItAPI.Model
{
    public record class Car
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public Guid Id { get; set; }
        public bool Available { get; set; }
        public string City { get; set; }
        public decimal RentPricePerDay { get; set; }
        public decimal InsurancePricePerDay { get; set; }

        //public Car() { }

        public Car(string brand, string model, int year, Guid id, bool available, string city, decimal rentPricePerDay, decimal insurancePricePerDay)
        {
            Brand = brand;
            Model = model;
            Year = year;
            Id = id;
            Available = available;
            City = city;
            RentPricePerDay = rentPricePerDay;
            InsurancePricePerDay = insurancePricePerDay;
        }
    }
}
