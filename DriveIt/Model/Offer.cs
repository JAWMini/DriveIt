using DriveIt.DTOs;

namespace DriveIt.Model
{
    public class Offer
    {
        public string IntegratorName { get; set; }
        public OfferDto OfferDetails { get; set; }
        public string IntegratorUrl { get; set; }
        public Offer(string integratorName, OfferDto offerDetails, string integratorUrl)
        {
            IntegratorName = integratorName;
            OfferDetails = offerDetails;
            IntegratorUrl = integratorUrl;
        }
    }
}
