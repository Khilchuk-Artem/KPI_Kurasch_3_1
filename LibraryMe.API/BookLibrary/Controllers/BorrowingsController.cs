using BookLibrary.BAL.Services.Interfaces;
using BookLibrary.DAL.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BorrowingsController : Controller
    {
        private readonly IBorrowingService _borrowingService;

        public BorrowingsController(IBorrowingService borrowingService)
        {
            _borrowingService = borrowingService;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetBorrowingById(Guid id)
        {
            var borrowing = await _borrowingService.GetBorrowingById(id);

            if (borrowing == null)
                return NotFound();

            return Ok(borrowing);
        }

        [HttpGet("summaries")]
        public async Task<IActionResult> GetBorrowingSummariesAsync([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1, [FromQuery] int? borrowerCardId = null, [FromQuery] string borrowerName = null, [FromQuery] bool hideReturned = false, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            var borrowings = await _borrowingService.GetBorrowingSummariesAsync(pageSize, pageNumber, borrowerCardId, borrowerName, hideReturned, startDate, endDate);

            return Ok(borrowings);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBorrowing([FromBody] CreateBorrowingDTO dto)
        {
            var createdId = await _borrowingService.CreateBorrowing(dto);

            return CreatedAtAction(nameof(GetBorrowingById), new { id = createdId }, dto);
        }

        [HttpPut("{id}/return")]
        public async Task<IActionResult> ReturnBorrowing(Guid id)
        {
            var success = await _borrowingService.ReturnBorrowing(id);

            if (!success)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBorrowing(Guid id)
        {
            var deletedBorrowing = await _borrowingService.DeleteBorrowing(id);

            if (deletedBorrowing == null)
                return NotFound();

            return Ok(deletedBorrowing);
        }
    }
}
