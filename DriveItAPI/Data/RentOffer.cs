namespace DriveItAPI.Data
{
    public record class RentOffer(Car Car, int Days)
    {
        public decimal RentPricePerDay { get; init; } = Car.RentPricePerDay;
        public decimal TotalPrice => RentPricePerDay * Days;
        public decimal InsurancePriccePerDay { get; init; } = Car.InsurancePricePerDay;

    }
}
