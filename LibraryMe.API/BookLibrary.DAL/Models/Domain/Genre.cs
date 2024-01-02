namespace BookLibrary.DAL.Models.Domain
{
    public class Genre
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Book> Books { get; set; }
    }
}
