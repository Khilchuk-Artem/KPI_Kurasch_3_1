using AutoMapper;
using BookLibrary.DAL.Data;
using BookLibrary.DAL.Models.Domain;
using BookLibrary.DAL.Models.DTO;
using BookLibrary.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.DAL.Repositories.Implementations
{
    public class BookRepository : IBookRepository
    {
        private readonly BookLibraryDbContext _dbContext;
        private readonly IMapper _mapper;

        public BookRepository(BookLibraryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<BookDTO?> GetBookById(Guid id)
        {
            var book = await _dbContext.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .Include(b => b.Image)
                .FirstOrDefaultAsync(b => !b.IsDeleted && b.Id == id);

            return book != null ? _mapper.Map<BookDTO>(book) : null;
        }

        public async Task<List<BookShortcutDTO>> GetBookShortcutsAsync(int pageSize = 5, int pageNumber = 1, string genreId = null)
        {
            var query = _dbContext.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .Include(b => b.Image)
                .Where(b => !b.IsDeleted).AsQueryable();

            if (genreId != null)
            {
                query = query.Where(b => b.Genres.Select(g => g.Id.ToString()).Contains(genreId));
            }

            var results = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(b => _mapper.Map<BookShortcutDTO>(b))
                .ToListAsync();

            return results;
        }

        public async Task<List<BookDTO>> GetBookSummariesAsync(int pageSize = 5, int pageNumber = 1, string genreId = null, string searchQuery = "")
        {
            var query = _dbContext.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .Include(b => b.Image)
                .Where(b => !b.IsDeleted).Where(b => b.Title.Contains(searchQuery))
                .AsQueryable();

            if (genreId != null)
            {
                query = query.Where(b => b.Genres.Select(g => g.Id.ToString()).Contains(genreId));
            }

            var results = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(b => new BookDTO()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description.Substring(0, Math.Min(b.Description.Length, 150)),
                    ImageUrl = b.Image.Url,
                    Genres = b.Genres.Select(g => new GenreDTO() { Id = g.Id, Name = g.Name }),
                    Authors = b.Authors.Select(a => new AuthorLinkDTO() { AuthorId = a.Id, Name = $"{a.Surname} {a.Name[0]}. {a.Patronymic[0]}" })
                })
                .ToListAsync();

            return results;
        }

        public async Task<Guid> CreateBook(CreateBookDTO dto)
        {
            var newBook = new Book
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                ImageId = dto.ImageId,
                Authors = _dbContext.Authors.Where(a => dto.AuthorIds.Contains(a.Id)).ToList(),
                Genres = _dbContext.Genres.Where(g => dto.GenreIds.Contains(g.Id)).ToList(),
            };

            await _dbContext.Books.AddAsync(newBook);
            await _dbContext.SaveChangesAsync();

            return newBook.Id;
        }

        public async Task<BookDTO?> UpdateBook(CreateBookDTO dto, Guid id)
        {
            var book = await _dbContext.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .Include(b => b.Image)
                .FirstOrDefaultAsync(b => !b.IsDeleted && b.Id == id);

            if (book == null)
                return null;

            _mapper.Map(dto, book);

            book.Authors = _dbContext.Authors.Where(a => dto.AuthorIds.Contains(a.Id)).ToList();
            book.Genres = _dbContext.Genres.Where(g => dto.GenreIds.Contains(g.Id)).ToList();

            _dbContext.Books.Update(book);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<BookDTO>(book);
        }

        public async Task<BookDTO?> DeleteBook(Guid id)
        {
            var book = await _dbContext.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .Include(b => b.Image)
                .FirstOrDefaultAsync(b => !b.IsDeleted && b.Id == id);

            if (book == null)
                return null;

            book.IsDeleted = true;

            _dbContext.Books.Update(book);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<BookDTO>(book);
        }
    }
}
