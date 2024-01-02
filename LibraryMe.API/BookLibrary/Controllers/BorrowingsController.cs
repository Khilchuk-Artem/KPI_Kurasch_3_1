using AutoMapper;
using BookLibrary.Data;
using BookLibrary.Models.Domain;
using BookLibrary.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BorrowingsController : Controller
    {
        private readonly BookLibraryDbContext _dbContext;
        private readonly IMapper _mapper;

        public BorrowingsController(BookLibraryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetBorrowingById(Guid id)
        {
            var borrowing = await _dbContext.Borrowings
                .Include(b => b.Books).ThenInclude(b => b.Image)
                .Include(b => b.Books)
                    .ThenInclude(book => book.Authors)
                .Include(b => b.VisitorsCard)
                .Include(b => b.BorrowingStatus)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (borrowing == null)
                return NotFound();
            if (borrowing.Books == null)
                return NotFound();
            var dto = _mapper.Map<BorrowingDTO>(borrowing);

            return Ok(dto);
        }

        [HttpGet("summaries")]
        public async Task<IActionResult> GetBorrowingSummariesAsync([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1, [FromQuery] int? borrowerCardId=null, 
            [FromQuery] string borrowerName = null, [FromQuery] bool hideReturned = false, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate=null)
        {

            var query = _dbContext.Borrowings
                .Include(b => b.VisitorsCard)
                .Include(b => b.BorrowingStatus)
                .OrderByDescending(b => b.DateCreated).AsQueryable();
            if (borrowerCardId.HasValue)
            {
                query = query.Where(b => b.VisitorsCardId == borrowerCardId);
            }
            if (hideReturned)
            {
                query = query.Where(b => b.BorrowingStatusId != Guid.Parse("76C30481-34B8-493E-857E-75622551A448"));
            }
            if (borrowerName != null)
            {
                query = query.Where(b => (b.VisitorsCard.Surname+ " "+ b.VisitorsCard.Name + " "+b.VisitorsCard.Patronymic).Contains(borrowerName));
            }
            if (startDate != null)
            {
                query = query.Where(b => b.DateCreated>=startDate.Value);
            }
            if (endDate != null)
            {
                query = query.Where(b =>b.DateCreated <= endDate.Value);
            }
            var results= await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(b => new BorrowingSummaryDTO
                {
                    BorrowingId = b.Id,
                    CreatedDate = DateOnly.FromDateTime(b.DateCreated.Date),
                    Status = b.BorrowingStatus.Name,
                    Creator = $"{b.VisitorsCard.Name} {b.VisitorsCard.Surname}"
                })
                .ToListAsync();

            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBorrowing([FromBody] CreateBorrowingDTO dto)
        {
            var books = await _dbContext.Books.Where(b => !b.IsDeleted && dto.BookIds.Contains(b.Id)).ToListAsync();
            if (books.Count != dto.BookIds.Count()) return BadRequest();
            var borrowing = new Borrowing()
            {
                DateCreated = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14),
                VisitorsCardId = dto.BorrowerId,
                BorrowingStatusId = Guid.Parse("73bb3243-c71c-4f1b-ba1f-f4fc56b5dee2"),
                Books = books
            };
            await _dbContext.Borrowings.AddAsync(borrowing);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBorrowingById), new { id = borrowing.Id }, borrowing.Id);
        }
        [HttpPut("{id}/return")]
        public async Task<IActionResult> ReturnBorrowing(Guid id)
        {
            var borrowing = await _dbContext.Borrowings.FindAsync(id);
            if(borrowing == null) return BadRequest();
            borrowing.BorrowingStatusId = Guid.Parse("76C30481-34B8-493E-857E-75622551A448");
            _dbContext.Update(borrowing);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBorrowing(Guid id)
        {
            var borrowing = await _dbContext.Borrowings
                .Include(b => b.Books)
                .Include(b => b.VisitorsCard)
                .Include(b => b.BorrowingStatus)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (borrowing == null)
                return NotFound();

            _dbContext.Borrowings.Remove(borrowing);
            await _dbContext.SaveChangesAsync();

            return Ok(_mapper.Map<BorrowingDTO>(borrowing));
        }
    }
}
