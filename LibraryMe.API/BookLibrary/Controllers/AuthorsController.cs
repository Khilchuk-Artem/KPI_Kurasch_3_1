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
    public class AuthorsController : Controller
    {
        private readonly BookLibraryDbContext _dbContext;
        private readonly IMapper _mapper;

        public AuthorsController(BookLibraryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAuthorById(Guid id)
        {
            var author = await _dbContext.Authors.Include(a => a.Image).Include(a=>a.Books).ThenInclude(b=>b.Image).Where(a => !a.IsDeleted).FirstOrDefaultAsync(a => a.Id == id);

            if (author == null) return NotFound();

            var dto = _mapper.Map<AuthorDTO>(author);

            return Ok(dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthorsAsync([FromQuery] int pageSize = 5, [FromQuery] int pageNumber = 1)
        {
            var results = await _dbContext.Authors.Include(a => a.Image).Where(a => !a.IsDeleted)
                .OrderBy(a => a.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(a => _mapper.Map<AuthorDTO>(a))
                .ToListAsync();

            return Ok(results);
        }
        [HttpGet("summaries")]
        public async Task<IActionResult> GetAuthorSummariesAsync([FromQuery] int pageSize = 5, [FromQuery] int pageNumber = 1, [FromQuery] string searchQuery="")
        {
            var results = await _dbContext.Authors
                .Include(a=>a.Image)
                .Where(a => !a.IsDeleted)
                .Where(a => (a.Name.ToLower() + " " + a.Patronymic.ToLower() + " " + a.Surname.ToLower()).Contains(searchQuery.ToLower()))
                .OrderBy(a => a.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(a => _mapper.Map<AuthorSummaryDTO>(a))
                .ToListAsync();

            return Ok(results);
        }
        [HttpGet("links")]
        public async Task<IActionResult> GetAuthorLinksAsync()
        {
            var results = await _dbContext.Authors
                .Include(a => a.Image)
                .Where(a => !a.IsDeleted)
                //.Where(a => (a.Name.ToLower() + " " + a.Patronymic.ToLower() + " " + a.Surname.ToLower()).Contains(searchQuery.ToLower()))
                .OrderBy(a => a.Name)
                //.Skip((pageNumber - 1) * pageSize)
                //.Take(pageSize)
                .Select(a => _mapper.Map<AuthorLinkDTO>(a))
                .ToListAsync();

            return Ok(results);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorDTO dto)
        {
            var author = _mapper.Map<Author>(dto);

            await _dbContext.Authors.AddAsync(author);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author.Id);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAuthor([FromBody] CreateAuthorDTO dto, Guid id)
        {
            var author = await _dbContext.Authors.Include(a => a.Image).Where(a => !a.IsDeleted).FirstOrDefaultAsync(a => a.Id == id);

            if (author == null) return NotFound();

            _mapper.Map(dto, author);

            _dbContext.Authors.Update(author);
            await _dbContext.SaveChangesAsync();

            return Ok(_mapper.Map<AuthorDTO>(author));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            var author = await _dbContext.Authors.Include(a=>a.Books).Include(a => a.Image).FirstOrDefaultAsync(a => a.Id == id);

            if (author == null) return NotFound();
            var booksWithoutAuthor = author.Books.Where(b => b.Authors.Where(a=>!a.IsDeleted).Count() == 1).ToList();

            author.IsDeleted = true;
            foreach(var b in booksWithoutAuthor)
            {
                b.IsDeleted = true;
                _dbContext.Books.Update(b);

            }
            _dbContext.Authors.Update(author);
            await _dbContext.SaveChangesAsync();

            return Ok(_mapper.Map<AuthorDTO>(author));
        }
    }
}
