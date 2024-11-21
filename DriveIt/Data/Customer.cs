namespace DriveIt.Data
{
    public class Customer : ApplicationUser
    {
        public DateTime DateOfBirth { get; set; }
        public int DriverLicenseYear { get; set; }
    }
}
