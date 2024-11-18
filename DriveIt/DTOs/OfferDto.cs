namespace DriveIt.DTOs
{
    public class OfferDto
    {
        public Guid Guid { get; set; }
        public decimal RentPrice { get; set; }
        public decimal InsurancePrice { get; set; }
        public int OfferTimeLimit { get; set; }

        public OfferDto(Guid guid, decimal rentPrice, decimal insurancePrice, int offerTimeLimit)
        {
            Guid = guid;
            RentPrice = rentPrice;
            InsurancePrice = insurancePrice;
            OfferTimeLimit = offerTimeLimit;
        }
    }
}
