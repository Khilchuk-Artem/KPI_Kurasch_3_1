using AutoMapper;
using BookLibrary.DAL.Data;
using BookLibrary.DAL.Models.Domain;
using BookLibrary.DAL.Models.DTO;
using BookLibrary.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.DAL.Repositories.Implementations
{
    public class BookmarkRepository : IBookmarkRepository
    {
        private readonly BookLibraryDbContext _dbContext;
        private readonly IMapper _mapper;

        public BookmarkRepository(BookLibraryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<BookmarkDTO>> GetBookmarksByUserId(Guid userId, string bookId = null)
        {
            var query = _dbContext.Bookmarks
                .Include(b => b.Book)
                .ThenInclude(book => book.Authors)
                .Where(b => b.UserId == userId).AsQueryable();

            if (bookId != null)
            {
                query = query.Where(b => b.BookId == Guid.Parse(bookId));
            }

            var bookmarks = await query.ToListAsync();

            return _mapper.Map<List<BookmarkDTO>>(bookmarks);
        }

        public async Task<Guid> CreateBookmark(CreateBookmarkDTO dto)
        {
            var bookmark = _mapper.Map<Bookmark>(dto);

            await _dbContext.Bookmarks.AddAsync(bookmark);
            await _dbContext.SaveChangesAsync();

            return bookmark.Id;
        }

        public async Task<BookmarkDTO?> DeleteBookmark(Guid id)
        {
            var bookmark = await _dbContext.Bookmarks.Include(b => b.Book).ThenInclude(b => b.Authors).FirstOrDefaultAsync(b => b.Id == id);

            if (bookmark == null)
                return null;

            var bookmarkDto = _mapper.Map<BookmarkDTO>(bookmark);

            _dbContext.Bookmarks.Remove(bookmark);
            await _dbContext.SaveChangesAsync();

            return bookmarkDto;
        }
    }
}
