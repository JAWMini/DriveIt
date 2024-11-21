namespace DriveItAPI.DTOs
{
    public class RentalDto(Guid id, CarDto carDto, Guid userId, DateTime startDate)
    {
        public Guid Id { get; set; } = id;
        public CarDto CarDto { get; set; } = carDto;
        public Guid UserId { get; set; } = userId;
        public DateTime StartDate { get; set; } = startDate;

        //public int Days { get; set; } = days;
    }
}
