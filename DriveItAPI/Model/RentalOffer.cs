namespace DriveItAPI.Model
{
    public record class RentalOffer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Car Car { get; set; }
        public decimal RentPricePerDay { get; init; }
        //public decimal TotalPrice => RentPricePerDay * Days;
        public decimal InsurancePriccePerDay { get; init; }
        public int OfferTimeLimit { get; init; } = 10;

        public RentalOffer() { }

        public RentalOffer(Car car)
        {
            Car = car;
            RentPricePerDay = car.RentPricePerDay;
            InsurancePriccePerDay = car.InsurancePricePerDay;
        }

    }
}
