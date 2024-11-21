namespace DriveIt.Model
{
    public class OfferRequest
    {
        public Guid CarId { get; set; }
        public int DrivingLicenseLength { get; set; }
        public int UserAge { get; set; }

        public OfferRequest(Guid carId, int drivingLicenseLength, int userAge)
        {
            CarId = carId;
            DrivingLicenseLength = drivingLicenseLength;
            UserAge = userAge;
        }

        //public OfferRequest(OfferRequestDto )

    }
}
