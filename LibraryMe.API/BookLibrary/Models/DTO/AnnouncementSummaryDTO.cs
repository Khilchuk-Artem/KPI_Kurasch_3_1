namespace BookLibrary.Models.DTO
{
    public class AnnouncementSummaryDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid AnnouncementId { get; set; }
    }
}
