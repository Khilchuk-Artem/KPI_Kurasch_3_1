namespace BookLibrary.DAL.Models.Domain
{
    public class Borrowing
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DueDate { get; set; }
        public int VisitorsCardId { get; set; }
        public Guid BorrowingStatusId {  get; set; }

        public VisitorsCard VisitorsCard { get; set; }
        public IEnumerable<Book> Books { get; set;}
        public BorrowingStatus BorrowingStatus { get; set; }
    }
}
