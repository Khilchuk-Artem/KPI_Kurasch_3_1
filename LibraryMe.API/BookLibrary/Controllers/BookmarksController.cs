using AutoMapper;
using BookLibrary.Data;
using BookLibrary.Models.Domain;
using BookLibrary.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookmarksController : Controller
    {
        private readonly BookLibraryDbContext _dbContext;
        private readonly IMapper _mapper;

        public BookmarksController(BookLibraryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetBookmarksByUserId(Guid userId, [FromQuery] string bookId=null)
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

            var bookmarkDtos = _mapper.Map<List<BookmarkDTO>>(bookmarks);

            return Ok(bookmarkDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookmark([FromBody] CreateBookmarkDTO dto)
        {
            var bookmark = _mapper.Map<Bookmark>(dto);

            await _dbContext.Bookmarks.AddAsync(bookmark);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookmarksByUserId), new { userId = bookmark.UserId }, _mapper.Map<BookmarkDTO>(bookmark));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBookmark(Guid id)
        {
            var bookmark = await _dbContext.Bookmarks.Include(b=>b.Book).ThenInclude(b=>b.Authors).FirstOrDefaultAsync(b=>b.Id==id);

            if (bookmark == null)
                return NotFound();
            var tmp = _mapper.Map<BookmarkDTO>(bookmark);
            _dbContext.Bookmarks.Remove(bookmark);
            await _dbContext.SaveChangesAsync();

            return Ok(tmp);
        }
    }
}
