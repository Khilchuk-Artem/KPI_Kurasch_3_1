namespace BookLibrary.Models.DTO
{
    public class AnnouncementDTO
    {
        public Guid Id { get; set; }
        public string Title {  get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
