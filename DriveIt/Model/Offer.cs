using DriveIt.DTOs;

namespace DriveIt.Model
{
    public class Offer
    {
        public Guid Id { get; set; }
        public decimal RentPrice { get; set; }
        public decimal InsurancePrice { get; set; }
        public int OfferTimeLimit { get; set; }
        public string IntegratorName { get; set; }
        public string IntegratorUrl { get; set; }

        public Offer(Guid id, decimal rentPrice, decimal insurancePrice, int offerTimeLimit, string companyName, string companyUrl)
        {
            Id = id;
            RentPrice = rentPrice;
            InsurancePrice = insurancePrice;
            OfferTimeLimit = offerTimeLimit;
            IntegratorName = companyName;
            IntegratorUrl = companyUrl;
        }

    }
}
