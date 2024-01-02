namespace BookLibrary.DAL.Models.DTO
{
    public class BorrowingDTO
    {
        public DateTime DateCreated { get; set; }
        public DateTime DueDate { get; set; }
        public string Borrower {  get; set; }
        public int BorrowerVisitorCardId { get; set; }
        public string Status { get; set; }
        public IEnumerable<BookShortcutDTO> Books { get; set; }
    }
}
