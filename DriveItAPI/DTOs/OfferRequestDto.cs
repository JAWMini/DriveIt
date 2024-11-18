namespace DriveItAPI.DTOs
{
    public class OfferRequestDto
    {
        public Guid CarId { get; set; }
        public int DriverLicenseLength { get; set; }
        public int UserAge { get; set; }
        public OfferRequestDto(Guid carId, int driverLicenseLength, int userAge)
        {
            CarId = carId;
            DriverLicenseLength = driverLicenseLength;
            UserAge = userAge;
        }
    }
}
