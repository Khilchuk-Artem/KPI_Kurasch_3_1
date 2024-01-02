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
    public class VisitorsCardsController : ControllerBase
    {
        private readonly BookLibraryDbContext _dbContext;
        private readonly IMapper _mapper;

        public VisitorsCardsController(BookLibraryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetVisitorCardById(int id)
        {
            var visitorCard = await _dbContext.VisitorsCards.Where(vc => !vc.IsDeleted)
                //.Include(vc => vc.VisitorMembership)
                .FirstOrDefaultAsync(vc => vc.Id == id);

            if (visitorCard == null) return NotFound();

            var dto = _mapper.Map<VisitorCardDTO>(visitorCard);

            return Ok(dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetVisitorCardsAsync()
        {
            var visitorCards = await _dbContext.VisitorsCards.Where(vc => !vc.IsDeleted)
                //.Include(vc => vc.VisitorMembership)
                .ToListAsync();

            var dtos = _mapper.Map<List<VisitorCardDTO>>(visitorCards);

            return Ok(dtos);
        }
        [HttpGet("shortcuts")]
        public async Task<IActionResult> GetVisitorCardShortcutsAsync([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1, [FromQuery] string query="")
        {
            var visitorCards = await _dbContext.VisitorsCards
                .Where(vc => !vc.IsDeleted).Where(vc=> (vc.Name.ToLower() + " " + vc.Patronymic.ToLower() + " " + vc.Surname.ToLower()).Contains(query.ToLower()))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                //.Include(vc => vc.VisitorMembership)
                .ToListAsync();

            var dtos = _mapper.Map<List<VisitorCardShortcutDTO>>(visitorCards);

            return Ok(dtos);
        }
        [HttpPost]
        public async Task<IActionResult> CreateVisitorCard([FromBody] CreateVisitorCardDTO dto)
        {
            var visitorCard = _mapper.Map<VisitorsCard>(dto);
            //visitorCard.VisitorMembershipId = Guid.Parse("DFCDCA9C-9858-416F-A49A-4843ED624E6C");
            await _dbContext.VisitorsCards.AddAsync(visitorCard);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVisitorCardById), new { id = visitorCard.Id }, dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateVisitorCard([FromBody] CreateVisitorCardDTO dto, int id)
        {
            var visitorCard = await _dbContext.VisitorsCards.Where(vc=>!vc.IsDeleted)
                //.Include(vc => vc.VisitorMembership)
                .FirstOrDefaultAsync(vc => vc.Id == id);

            if (visitorCard == null) return NotFound();

            _mapper.Map(dto, visitorCard);

            _dbContext.VisitorsCards.Update(visitorCard);
            await _dbContext.SaveChangesAsync();

            return Ok(_mapper.Map<VisitorCardDTO>(visitorCard));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteVisitorCard(int id)
        {
            var visitorCard = await _dbContext.VisitorsCards.Where(vc => !vc.IsDeleted)
                //.Include(vc => vc.VisitorMembership)
                .FirstOrDefaultAsync(vc => vc.Id == id);

            if (visitorCard == null) return NotFound();

            visitorCard.IsDeleted = true;

            _dbContext.VisitorsCards.Update(visitorCard);
            await _dbContext.SaveChangesAsync();

            return Ok(_mapper.Map<VisitorCardDTO>(visitorCard));
        }
    }
}
