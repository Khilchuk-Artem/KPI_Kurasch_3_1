using BookLibrary.BAL.Services.Interfaces;
using BookLibrary.DAL.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorRepo;

        public AuthorsController(IAuthorService authorRepo)
        {
            _authorRepo = authorRepo;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAuthorById(Guid id)
        {
            var author = await _authorRepo.GetAuthorById(id);

            if (author == null) return NotFound();

            return Ok(author);
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthorsAsync([FromQuery] int pageSize = 5, [FromQuery] int pageNumber = 1)
        {
            var authors = await _authorRepo.GetAuthorsAsync(pageSize, pageNumber);

            return Ok(authors);
        }

        [HttpGet("summaries")]
        public async Task<IActionResult> GetAuthorSummariesAsync([FromQuery] int pageSize = 5, [FromQuery] int pageNumber = 1, [FromQuery] string searchQuery = "")
        {
            var summaries = await _authorRepo.GetAuthorSummariesAsync(pageSize, pageNumber, searchQuery);

            return Ok(summaries);
        }

        [HttpGet("links")]
        public async Task<IActionResult> GetAuthorLinksAsync()
        {
            var links = await _authorRepo.GetAuthorLinksAsync();

            return Ok(links);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorDTO dto)
        {
            var createdId = await _authorRepo.CreateAuthor(dto);

            return CreatedAtAction(nameof(GetAuthorById), new { id = createdId }, dto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAuthor([FromBody] CreateAuthorDTO dto, Guid id)
        {
            var updatedAuthor = await _authorRepo.UpdateAuthor(dto, id);

            if (updatedAuthor == null) return NotFound();

            return Ok(updatedAuthor);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            var deletedAuthor = await _authorRepo.DeleteAuthor(id);

            if (deletedAuthor == null) return NotFound();

            return Ok(deletedAuthor);
        }
    }
}
