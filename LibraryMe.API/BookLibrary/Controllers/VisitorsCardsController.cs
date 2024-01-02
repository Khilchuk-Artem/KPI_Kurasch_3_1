using BookLibrary.BAL.Services.Interfaces;
using BookLibrary.DAL.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisitorsCardsController : ControllerBase
    {
        private readonly IVisitorCardService _visitorCardService;

        public VisitorsCardsController(IVisitorCardService visitorCardService)
        {
            _visitorCardService = visitorCardService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetVisitorCardById(int id)
        {
            var visitorCard = await _visitorCardService.GetVisitorCardByIdAsync(id);

            if (visitorCard == null) return NotFound();

            return Ok(visitorCard);
        }

        [HttpGet]
        public async Task<IActionResult> GetVisitorCardsAsync()
        {
            var visitorCards = await _visitorCardService.GetVisitorCardsAsync();

            return Ok(visitorCards);
        }

        [HttpGet("shortcuts")]
        public async Task<IActionResult> GetVisitorCardShortcutsAsync([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1, [FromQuery] string query = "")
        {
            var shortcuts = await _visitorCardService.GetVisitorCardShortcutsAsync(pageSize, pageNumber, query);

            return Ok(shortcuts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVisitorCard([FromBody] CreateVisitorCardDTO dto)
        {
            var createdId = await _visitorCardService.CreateVisitorCardAsync(dto);

            return CreatedAtAction(nameof(GetVisitorCardById), new { id = createdId }, dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateVisitorCard([FromBody] CreateVisitorCardDTO dto, int id)
        {
            var updatedVisitorCard = await _visitorCardService.UpdateVisitorCardAsync(dto, id);

            if (updatedVisitorCard == null) return NotFound();

            return Ok(updatedVisitorCard);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteVisitorCard(int id)
        {
            var deletedVisitorCard = await _visitorCardService.DeleteVisitorCardAsync(id);

            if (deletedVisitorCard == null) return NotFound();

            return Ok(deletedVisitorCard);
        }
    }
}