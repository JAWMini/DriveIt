namespace DriveItAPI.Model
{
    public record class Rent(Guid Id, Car Car, Guid UserId, DateTime StartDate, int Days)
    {
    }
}
