namespace BookLibrary.DAL.Models.DTO
{
    public class CreateBookmarkDTO
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
    }
}
