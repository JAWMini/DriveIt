namespace DriveItAPI.DTOs
{
    public record class CarDto(string Brand, string Model, int Year, Guid Id, string City)
    {
    }
}
