using BookLibrary.BAL.Services.Interfaces;
using BookLibrary.DAL.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookmarksController : ControllerBase
    {
        private readonly IBookmarkService _bookmarkService;

        public BookmarksController(IBookmarkService bookmarkService)
        {
            _bookmarkService = bookmarkService;
        }

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetBookmarksByUserId(Guid userId, [FromQuery] string bookId = null)
        {
            var bookmarks = await _bookmarkService.GetBookmarksByUserId(userId, bookId);

            return Ok(bookmarks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookmark([FromBody] CreateBookmarkDTO dto)
        {
            var createdId = await _bookmarkService.CreateBookmark(dto);

            return CreatedAtAction(nameof(GetBookmarksByUserId), new { userId = dto.UserId }, dto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBookmark(Guid id)
        {
            var deletedBookmark = await _bookmarkService.DeleteBookmark(id);

            if (deletedBookmark == null) return NotFound();

            return Ok(deletedBookmark);
        }
    }
}
