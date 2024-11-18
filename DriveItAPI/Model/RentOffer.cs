namespace DriveItAPI.Model
{
    public record class RentOffer(Car Car)
    {
        public Guid Id { get; } = new();
        public decimal RentPricePerDay { get; init; } = Car.RentPricePerDay;
        //public decimal TotalPrice => RentPricePerDay * Days;
        public decimal InsurancePriccePerDay { get; init; } = Car.InsurancePricePerDay;
        public int OfferTimeLimit { get; init; } = 10;

    }
}
