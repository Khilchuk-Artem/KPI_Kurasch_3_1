using BookLibrary.DAL.Models.DTO;

namespace BookLibrary.DAL.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<BookDTO?> GetBookById(Guid id);
        Task<List<BookShortcutDTO>> GetBookShortcutsAsync(int pageSize = 5, int pageNumber = 1, string genreId = null);
        Task<List<BookDTO>> GetBookSummariesAsync(int pageSize = 5, int pageNumber = 1, string genreId = null, string searchQuery = "");
        Task<Guid> CreateBook(CreateBookDTO dto);
        Task<BookDTO?> UpdateBook(CreateBookDTO dto, Guid id);
        Task<BookDTO?> DeleteBook(Guid id);
    }
}
