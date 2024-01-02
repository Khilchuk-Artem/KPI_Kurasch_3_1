using BookLibrary.BAL.Services.Interfaces;
using BookLibrary.DAL.Repositories.Interfaces;
using BookLibrary.DAL.Models.DTO;

namespace BookLibrary.BAL.Services.Implementations
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IBookmarkRepository _bookmarkRepo;

        public BookmarkService(IBookmarkRepository bookmarkRepo)
        {
            _bookmarkRepo = bookmarkRepo;
        }

        public async Task<List<BookmarkDTO>> GetBookmarksByUserId(Guid userId, string bookId = null)
        {
            return await _bookmarkRepo.GetBookmarksByUserId(userId, bookId);
        }

        public async Task<Guid> CreateBookmark(CreateBookmarkDTO dto)
        {
            return await _bookmarkRepo.CreateBookmark(dto);
        }

        public async Task<BookmarkDTO?> DeleteBookmark(Guid id)
        {
            return await _bookmarkRepo.DeleteBookmark(id);
        }
    }
}
