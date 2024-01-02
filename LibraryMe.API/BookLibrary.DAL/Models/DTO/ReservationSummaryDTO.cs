namespace BookLibrary.DAL.Models.DTO
{
    public class ReservationSummaryDTO
    {
        public int ReservationId {  get; set; }
        public DateTime CreatedTime {  get; set; }
        public string Status {  get; set; }
        public string Creator { get; set; }
    }
}
