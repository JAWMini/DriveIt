namespace DriveItAPI.Model
{
    public record class Rental
    {
        public Guid Id { get; set; }
        public Car Car { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }

        public Rental() { }

        public Rental(Guid id, Car car, Guid userId, DateTime startDate)
        {
            Id = id;
            Car = car;
            UserId = userId;
            StartDate = startDate;
        }
    }
}
