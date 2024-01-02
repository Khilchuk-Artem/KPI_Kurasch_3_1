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
    public class AnnouncementsController : Controller
    {
        private readonly BookLibraryDbContext _dbContext;
        private readonly IMapper _mapper;

        public AnnouncementsController(BookLibraryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAnnouncementById(Guid id)
        {
            var announcement = await _dbContext.Announcements.Where(a => !a.IsDeleted).FirstOrDefaultAsync(a => a.Id == id);

            if (announcement == null) return NotFound();

            var dto = _mapper.Map<AnnouncementDTO>(announcement);

            return Ok(dto);
        }
        [HttpGet]
        public async Task<IActionResult> GetAnnouncementsAsync([FromQuery] int pageSize=5, [FromQuery] int pageNumber=1) 
        {
            var results = await _dbContext.Announcements.Where(a => !a.IsDeleted).OrderByDescending(a => a.CreatedDate).Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(a => _mapper.Map<AnnouncementDTO>(a)).ToListAsync();

            return Ok(results);
        }
        [HttpGet("summaries")]
        public async Task<IActionResult> GetAnnouncementSummariesAsync([FromQuery] int pageSize = 5, [FromQuery] int pageNumber = 1)
        {
            var results = await _dbContext.Announcements
                .Where(a => !a.IsDeleted)
                .OrderByDescending(a => a.CreatedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new AnnouncementDTO()
                {
                    Id=a.Id,
                    Title = a.Title,
                    Content = a.Content.Substring(0, Math.Min(a.Content.Length, 300)),
                    DateCreated=a.CreatedDate
                }).ToListAsync();

            return Ok(results);
        }
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> CreateAnnouncement([FromBody] AnnouncementDTO dto)
        {
            var announcement = _mapper.Map<Announcement>(dto);
            announcement.CreatedDate = DateTime.Now;
            await _dbContext.Announcements.AddAsync(announcement);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAnnouncementById), new { id = announcement.Id }, dto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAnnouncement([FromBody] UpdateAnnouncementDTO dto, Guid id)
        {
            var announcement = await _dbContext.Announcements.Where(a=>!a.IsDeleted).FirstOrDefaultAsync(a => a.Id == id);

            if (announcement == null) return NotFound();

            announcement.Title = dto.Title;
            announcement.Content=dto.Content;

            _dbContext.Announcements.Update(announcement);
            await _dbContext.SaveChangesAsync();

            return Ok(_mapper.Map<AnnouncementDTO>(announcement));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAnnouncement(Guid id)
        {
            var announcement = await _dbContext.Announcements.FirstOrDefaultAsync(a => a.Id == id);

            if (announcement == null) return NotFound();

            announcement.IsDeleted = true;

            _dbContext.Announcements.Update(announcement);
            await _dbContext.SaveChangesAsync();

            return Ok(_mapper.Map<AnnouncementDTO>(announcement));
        }
    }
}
