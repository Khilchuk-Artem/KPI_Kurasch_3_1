namespace BookLibrary.DAL.Models.DTO
{
    public class BookmarkDTO
    {
            public Guid Id { get; set; }
            public Guid BookId { get; set; }
            public string Title { get; set; }
    }
}
