namespace BookLibrary.DAL.Models.Domain
{
    public class Bookmark
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }

        public Book Book { get; set; }
    }
}
