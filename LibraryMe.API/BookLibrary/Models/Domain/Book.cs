namespace BookLibrary.Models.Domain
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public Guid ImageId { get; set; }


        public IEnumerable<Author> Authors {get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<Borrowing> Borrowings { get; set; }
        public IEnumerable<Reservation> Reservations { get; set; }
        public Image Image { get; set; }
    }
}
