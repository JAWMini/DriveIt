namespace DriveIt.DTOs
{
    public class OfferRequestDto
    {
        public Guid CarId { get; set; }
        public int DrivingLicenseLength { get; set; }
        public int UserAge { get; set; }

        public OfferRequestDto(Guid carId, int drivingLicenseLength, int userAge)
        {
            CarId = carId;
            DrivingLicenseLength = drivingLicenseLength;
            UserAge = userAge;
        }   
    }
}
