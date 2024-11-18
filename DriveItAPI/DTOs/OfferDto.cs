namespace DriveItAPI.DTOs
{
    public class OfferDto
    {
        public Guid Id { get; set; }
        public decimal RentPrice { get; set; }
        public decimal InsurancePrice { get; set; }
        public int OfferTimeLimit { get; set; }
        public string IntegratorName { get; set; } = "DriveIt";
        // TODO: zmienić adres
        public string IntegratorUrl { get; set; } = "https://localhost:7289";

        public OfferDto(Guid id, decimal rentPrice, decimal insurancePrice, int offerTimeLimit)
        {
            Id = id;
            RentPrice = rentPrice;
            InsurancePrice = insurancePrice;
            OfferTimeLimit = offerTimeLimit;
        }
    }
}
