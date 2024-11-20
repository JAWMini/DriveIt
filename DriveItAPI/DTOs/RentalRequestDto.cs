namespace DriveItAPI.DTOs
{
    public class RentalRequestDto(Guid offerId, Guid userId)
    {
        public Guid OfferId { get; set; } = offerId;
        public Guid UserId { get; set; } = userId;
    }
}
