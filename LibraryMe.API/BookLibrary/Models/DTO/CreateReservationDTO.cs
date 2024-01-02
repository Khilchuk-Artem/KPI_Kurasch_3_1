namespace BookLibrary.Models.DTO
{
    public class CreateReservationDTO
    {
        public int ReservatorId { get; set; }
        public IEnumerable<Guid> BookIds { get; set; }
    }
}
