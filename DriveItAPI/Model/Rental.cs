namespace DriveItAPI.Model
{
    public record class Rental(Guid Id, Car Car, Guid UserId, DateTime StartDate)
    {
    }
}
