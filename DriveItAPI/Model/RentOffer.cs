namespace DriveItAPI.Model
{
    public record class RentOffer
    {
        public Guid Id { get; set; } = new();
        public Car Car { get; set; }
        public decimal RentPricePerDay { get; init; }
        //public decimal TotalPrice => RentPricePerDay * Days;
        public decimal InsurancePriccePerDay { get; init; }
        public int OfferTimeLimit { get; init; } = 10;

        public RentOffer() { }

        public RentOffer(Car car)
        {
            Car = car;
            RentPricePerDay = car.RentPricePerDay;
            InsurancePriccePerDay = car.InsurancePricePerDay;
        }

    }
}
