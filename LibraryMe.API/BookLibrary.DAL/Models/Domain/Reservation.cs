namespace BookLibrary.DAL.Models.Domain
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateAccepted { get; set; }
        public DateTime? DateCheckedOut { get; set; }
        public int ReservatorId { get; set; }
        public Guid ReservationStatusId { get; set; }

        public IEnumerable<Book> Books { get; set; }
        public ReservationStatus ReservationStatus { get; set; }
        public VisitorsCard Reservator { get; set; }
    }
}
