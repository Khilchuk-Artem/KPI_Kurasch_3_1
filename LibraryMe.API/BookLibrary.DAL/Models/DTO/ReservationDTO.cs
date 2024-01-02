namespace BookLibrary.DAL.Models.DTO
{
    public class ReservationDTO
    {
        public int ReservationId { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? AcceptedTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public string Status { get; set; }
        public  IEnumerable<BookShortcutDTO> Books { get; set; }
    }
}
