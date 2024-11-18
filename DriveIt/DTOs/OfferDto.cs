namespace DriveIt.DTOs
{
    public class OfferDto
    {
        public Guid Id { get; set; }
        public decimal RentPrice { get; set; }
        public decimal InsurancePrice { get; set; }
        public int OfferTimeLimit { get; set; }

        public string IntegratorName { get; set; }
        public string IntegratorUrl { get; set; }

        public OfferDto(Guid id, decimal rentPrice, decimal insurancePrice, int offerTimeLimit, string integratorName, string integratorUrl)
        {
            Id = id;
            RentPrice = rentPrice;
            InsurancePrice = insurancePrice;
            OfferTimeLimit = offerTimeLimit;
            IntegratorName = integratorName;
            IntegratorUrl = integratorUrl;
        }
    }
}
