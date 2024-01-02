namespace BookLibrary.DAL.Models.DTO
{
    public class BookShortcutDTO
    {
        public Guid Id { get; set; }
        public string BookId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<AuthorLinkDTO> Authors { get; set; }
    }
}
