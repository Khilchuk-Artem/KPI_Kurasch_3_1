using AutoMapper;
using BookLibrary.Data;
using BookLibrary.Models.Domain;
using BookLibrary.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly BookLibraryDbContext _dbContext;
        private readonly IMapper _mapper;

        public ReservationsController(BookLibraryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            var reservation = await _dbContext.Reservations
                .Include(r => r.Books)
                .ThenInclude(b=>b.Image)
                .Include(r => r.ReservationStatus)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
                return NotFound();

            var dto = _mapper.Map<ReservationDTO>(reservation);

            return Ok(dto);
        }

        [HttpGet("summaries")]
        public async Task<IActionResult> GetReservationSummariesAsync([FromQuery] int pageSize = 5, [FromQuery] int pageNumber = 1, [FromQuery] string? searchQuery = "", [FromQuery] int? visitorCardId=null)
        {

            var query = _dbContext.Reservations
                .Where(r => r.Id.ToString().Contains(searchQuery)).AsQueryable();
            if (visitorCardId.HasValue)
            {
                query=query.Where(r=>r.Reservator.Id==visitorCardId.Value); 
            }
            var results= await query
                .OrderByDescending(r=>r.DateCreated)
                .Include(r => r.ReservationStatus)
                .Include(r=>r.Reservator)
                .OrderBy(r => r.DateCreated)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(r => _mapper.Map<ReservationSummaryDTO>(r))
                .ToListAsync();

            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationDTO dto)
        {
            var books = await _dbContext.Books.Where(b => !b.IsDeleted && dto.BookIds.Contains(b.Id)).ToListAsync();
            if (books.Count != dto.BookIds.Count()) return BadRequest();

            var reservation = new Reservation()
            {
                DateCreated = DateTime.Now,
                ReservatorId = dto.ReservatorId,
                ReservationStatusId = Guid.Parse("929B2083-C7B5-4D8C-B216-F02B0DC65AF7"), // Set your default reservation status ID
                Books = books
            };

            await _dbContext.Reservations.AddAsync(reservation);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReservationById), new { id = reservation.Id }, dto);
        }

        [HttpPut("{id:int}/decline")]
        public async Task<IActionResult> DeclineReservation(int id)
        {
            var reservation = await _dbContext.Reservations
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
                return NotFound();
            if (reservation.ReservationStatusId != Guid.Parse("929B2083-C7B5-4D8C-B216-F02B0DC65AF7"))
                return BadRequest();
            reservation.ReservationStatusId = Guid.Parse("5b0b6de5-7db3-4fb1-9173-8a1f4c2ff9c9");

            _dbContext.Reservations.Update(reservation);
            await _dbContext.SaveChangesAsync();

            var updatedDto = _mapper.Map<ReservationDTO>(reservation);

            return Ok(updatedDto);
        }
        [HttpPut("{id:int}/accept")]
        public async Task<IActionResult> AcceptReservation(int id)
        {
            var reservation = await _dbContext.Reservations
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
                return NotFound();
            if (reservation.ReservationStatusId != Guid.Parse("929B2083-C7B5-4D8C-B216-F02B0DC65AF7"))
                return BadRequest();
            reservation.ReservationStatusId = Guid.Parse("70B5342F-F380-47CF-B9D1-5E3F42A15FF0");
            reservation.DateAccepted= DateTime.Now;
            _dbContext.Reservations.Update(reservation);
            await _dbContext.SaveChangesAsync();

            var updatedDto = _mapper.Map<ReservationDTO>(reservation);

            return Ok(updatedDto);
        }
        [HttpPut("{id:int}/checkout")]
        public async Task<IActionResult> CkeckOutReservation(int id)
        {
            var reservation = await _dbContext.Reservations
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
                return NotFound();
            if (reservation.ReservationStatusId != Guid.Parse("70B5342F-F380-47CF-B9D1-5E3F42A15FF0"))
                return BadRequest();
            reservation.ReservationStatusId = Guid.Parse("CC7951BD-8930-48C0-B7CE-AA60274C610E");
            reservation.DateCheckedOut = DateTime.Now;
            _dbContext.Reservations.Update(reservation);
            
            await _dbContext.SaveChangesAsync();

            var updatedDto = _mapper.Map<ReservationDTO>(reservation);

            return Ok(updatedDto);
        }
    }
}
