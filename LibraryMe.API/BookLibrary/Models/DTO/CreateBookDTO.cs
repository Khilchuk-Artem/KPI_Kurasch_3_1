namespace BookLibrary.Models.DTO
{
    public class CreateBookDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid ImageId { get; set; }

        public List<Guid> AuthorIds { get; set; }
        public List<Guid> GenreIds { get; set; }
    }
}
