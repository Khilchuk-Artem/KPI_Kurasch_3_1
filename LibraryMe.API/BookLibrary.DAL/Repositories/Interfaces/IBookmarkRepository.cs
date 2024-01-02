using BookLibrary.DAL.Models.DTO;

namespace BookLibrary.DAL.Repositories.Interfaces
{
    public interface IBookmarkRepository
    {
        Task<List<BookmarkDTO>> GetBookmarksByUserId(Guid userId, string bookId = null);
        Task<Guid> CreateBookmark(CreateBookmarkDTO dto);
        Task<BookmarkDTO?> DeleteBookmark(Guid id);
    }

}
