using BookLibrary.BAL.Services.Interfaces;
using BookLibrary.DAL.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            var book = await _bookService.GetBookById(id);

            if (book == null) return NotFound();

            return Ok(book);
        }

        [HttpGet("shortcuts")]
        public async Task<IActionResult> GetBookShortcutsAsync([FromQuery] int pageSize = 5, [FromQuery] int pageNumber = 1, [FromQuery] string genreId = null)
        {
            var books = await _bookService.GetBookShortcutsAsync(pageSize, pageNumber, genreId);

            return Ok(books);
        }

        [HttpGet("summaries")]
        public async Task<IActionResult> GetBookSummariesAsync([FromQuery] int pageSize = 5, [FromQuery] int pageNumber = 1, [FromQuery] string genreId = null, [FromQuery] string searchQuery = "")
        {
            var books = await _bookService.GetBookSummariesAsync(pageSize, pageNumber, genreId, searchQuery);

            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDTO dto)
        {
            var createdId = await _bookService.CreateBook(dto);

            return CreatedAtAction(nameof(GetBookById), new { id = createdId }, dto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateBook([FromBody] CreateBookDTO dto, Guid id)
        {
            var updatedBook = await _bookService.UpdateBook(dto, id);

            if (updatedBook == null) return NotFound();

            return Ok(updatedBook);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var deletedBook = await _bookService.DeleteBook(id);

            if (deletedBook == null) return NotFound();

            return Ok(deletedBook);
        }
    }
}
