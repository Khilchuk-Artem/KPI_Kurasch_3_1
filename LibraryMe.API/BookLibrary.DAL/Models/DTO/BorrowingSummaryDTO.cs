namespace BookLibrary.DAL.Models.DTO
{
    public class BorrowingSummaryDTO
    {
        public Guid BorrowingId {  get; set; }
        public DateOnly CreatedDate { get; set; }
        public string Status { get; set; }
        public string Creator {  get; set; }
    }
}
