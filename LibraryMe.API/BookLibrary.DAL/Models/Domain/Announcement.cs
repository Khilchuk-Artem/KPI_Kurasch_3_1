namespace BookLibrary.DAL.Models.Domain
{
    public class Announcement
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
    }
}
