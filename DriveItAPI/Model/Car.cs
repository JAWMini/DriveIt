namespace DriveItAPI.Model
{
    public record class Car(string Brand, string Model, int Year, Guid Id,
        bool Available, string City, decimal RentPricePerDay, decimal InsurancePricePerDay)
    {
    }
}
