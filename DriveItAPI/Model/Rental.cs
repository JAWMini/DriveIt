namespace DriveItAPI.Model
{
    public record class Rental
    {
        public Guid Id { get; set; }
        public Car Car { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime? ReturnDate { get; set; }
        public DateTime? AcceptedDate { get; set; }

        public RentalStatus Status { get; set; } = RentalStatus.Rented;
        public decimal? Cost { get; set; }

        public Rental() { }

        public Rental(Guid id, Car car, Guid userId, DateTime startDate)
        {
            Id = id;
            Car = car;
            UserId = userId;
            StartDate = startDate;
        }
    }

    public enum RentalStatus
    {
        Rented,
        AcceptanceRequested,
        Accepted,        
    }
}
