using BookLibrary.BAL.Services.Interfaces;
using BookLibrary.DAL.Repositories.Interfaces;
using BookLibrary.DAL.Models.DTO;

namespace BookLibrary.BAL.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepo;

        public BookService(IBookRepository bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public async Task<BookDTO?> GetBookById(Guid id)
        {
            return await _bookRepo.GetBookById(id);
        }

        public async Task<List<BookShortcutDTO>> GetBookShortcutsAsync(int pageSize = 5, int pageNumber = 1, string genreId = null)
        {
            return await _bookRepo.GetBookShortcutsAsync(pageSize, pageNumber, genreId);
        }

        public async Task<List<BookDTO>> GetBookSummariesAsync(int pageSize = 5, int pageNumber = 1, string genreId = null, string searchQuery = "")
        {
            return await _bookRepo.GetBookSummariesAsync(pageSize, pageNumber, genreId, searchQuery);
        }

        public async Task<Guid> CreateBook(CreateBookDTO dto)
        {
            return await _bookRepo.CreateBook(dto);
        }

        public async Task<BookDTO?> UpdateBook(CreateBookDTO dto, Guid id)
        {
            return await _bookRepo.UpdateBook(dto, id);
        }

        public async Task<BookDTO?> DeleteBook(Guid id)
        {
            return await _bookRepo.DeleteBook(id);
        }
    }
}
