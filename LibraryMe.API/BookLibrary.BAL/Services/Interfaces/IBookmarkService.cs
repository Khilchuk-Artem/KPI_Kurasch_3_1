using BookLibrary.DAL.Models.DTO;

namespace BookLibrary.BAL.Services.Interfaces
{
    public interface IBookmarkService
    {
        Task<List<BookmarkDTO>> GetBookmarksByUserId(Guid userId, string bookId = null);
        Task<Guid> CreateBookmark(CreateBookmarkDTO dto);
        Task<BookmarkDTO?> DeleteBookmark(Guid id);
    }
}
