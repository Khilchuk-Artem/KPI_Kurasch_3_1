using BookLibrary.BAL.Services.Interfaces;
using BookLibrary.DAL.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IAnnouncementService _announcementRepo;

        public AnnouncementsController(IAnnouncementService announcementRepo)
        {
            _announcementRepo = announcementRepo;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAnnouncementById(Guid id)
        {
            var announcement = await _announcementRepo.GetAnnouncementById(id);

            if (announcement == null) return NotFound();

            return Ok(announcement);
        }

        [HttpGet]
        public async Task<IActionResult> GetAnnouncementsAsync([FromQuery] int pageSize = 5, [FromQuery] int pageNumber = 1)
        {
            var announcements = await _announcementRepo.GetAnnouncementsAsync(pageSize, pageNumber);

            return Ok(announcements);
        }

        [HttpGet("summaries")]
        public async Task<IActionResult> GetAnnouncementSummariesAsync([FromQuery] int pageSize = 5, [FromQuery] int pageNumber = 1)
        {
            var summaries = await _announcementRepo.GetAnnouncementSummariesAsync(pageSize, pageNumber);

            return Ok(summaries);
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> CreateAnnouncement([FromBody] AnnouncementDTO dto)
        {
            var createdId = await _announcementRepo.CreateAnnouncement(dto);

            return CreatedAtAction(nameof(GetAnnouncementById), new { id = createdId }, dto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAnnouncement([FromBody] UpdateAnnouncementDTO dto, Guid id)
        {
            var updatedAnnouncement = await _announcementRepo.UpdateAnnouncement(dto, id);

            if (updatedAnnouncement == null) return NotFound();

            return Ok(updatedAnnouncement);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAnnouncement(Guid id)
        {
            var deletedAnnouncement = await _announcementRepo.DeleteAnnouncement(id);

            if (deletedAnnouncement == null) return NotFound();

            return Ok(deletedAnnouncement);
        }
    }

}
