using BookLibrary.Models.Domain;

namespace BookLibrary.Models.DTO
{
    public class BookDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public IEnumerable<GenreDTO> Genres { get; set; }
        public IEnumerable<AuthorLinkDTO> Authors { get; set; }
    }
}
