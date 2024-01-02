using AutoMapper;
using BookLibrary.Data;
using BookLibrary.Models.Domain;
using BookLibrary.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookLibraryDbContext _dbContext;
        private readonly IMapper _mapper;

        public BooksController(BookLibraryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            var book = await _dbContext.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .Include(b => b.Image)
                .FirstOrDefaultAsync(b => !b.IsDeleted && b.Id == id);

            if (book == null) return NotFound();
            


            var dto = _mapper.Map<BookDTO>(book);

            return Ok(dto);
        }

        [HttpGet("shortcuts")]
        public async Task<IActionResult> GetBookShortcutsAsync([FromQuery] int pageSize = 5, [FromQuery] int pageNumber = 1, [FromQuery] string genreId=null)
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
                //.OrderBy(b => b.Title)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(b => _mapper.Map<BookShortcutDTO>(b))
                .ToListAsync();

            return Ok(results);
        }
        [HttpGet("summaries")]
        public async Task<IActionResult> GetBookSummariesAsync([FromQuery] int pageSize = 5, [FromQuery] int pageNumber = 1, [FromQuery] string genreId = null, [FromQuery] string searchQuery = "")
        {
            var query = _dbContext.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .Include(b => b.Image)
                .Where(b => !b.IsDeleted).Where(b=>b.Title.Contains(searchQuery))
                .AsQueryable();
            if (genreId != null)
            {
                query=query.Where(b => b.Genres.Select(g => g.Id.ToString()).Contains(genreId));
            }
            var results= await query
                //.OrderBy(b => b.Title)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(b => new BookDTO()
                {
                    Id=b.Id,
                    Title = b.Title,
                    Description = b.Description.Substring(0, Math.Min(b.Description.Length, 150)),
                    ImageUrl = b.Image.Url,
                    Genres = b.Genres.Select(g => new GenreDTO() { Id = g.Id, Name = g.Name }),
                    Authors = b.Authors.Select(a => new AuthorLinkDTO() { AuthorId = a.Id, Name = $"{a.Surname} {a.Name[0]}. {a.Patronymic[0]}" })})
                .ToListAsync();
            return Ok(results);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDTO dto)
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

            return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id }, dto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateBook([FromBody] CreateBookDTO dto, Guid id)
        {
            var book = await _dbContext.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .Include(b => b.Image)
                .FirstOrDefaultAsync(b => !b.IsDeleted && b.Id == id);

            if (book == null)
                return NotFound();

            _mapper.Map(dto, book);

            book.Authors = _dbContext.Authors.Where(a => dto.AuthorIds.Contains(a.Id)).ToList();
            book.Genres = _dbContext.Genres.Where(g => dto.GenreIds.Contains(g.Id)).ToList();

            _dbContext.Books.Update(book);
            await _dbContext.SaveChangesAsync();

            return Ok(_mapper.Map<BookDTO>(book));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var book = await _dbContext.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .Include(b => b.Image)
                .FirstOrDefaultAsync(b => !b.IsDeleted && b.Id == id);

            if (book == null)
                return NotFound();

            book.IsDeleted = true;

            _dbContext.Books.Update(book);
            await _dbContext.SaveChangesAsync();

            return Ok(_mapper.Map<BookDTO>(book));
        }
    }
}
