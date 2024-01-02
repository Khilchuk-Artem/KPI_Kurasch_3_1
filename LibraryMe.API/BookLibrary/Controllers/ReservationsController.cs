using BookLibrary.BAL.Services.Interfaces;
using BookLibrary.DAL.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            var reservation = await _reservationService.GetReservationById(id);

            return reservation != null ? Ok(reservation) : NotFound();
        }

        [HttpGet("summaries")]
        public async Task<IActionResult> GetReservationSummariesAsync([FromQuery] int pageSize = 5, [FromQuery] int pageNumber = 1, [FromQuery] string? searchQuery = "", [FromQuery] int? visitorCardId = null)
        {
            var summaries = await _reservationService.GetReservationSummaries(pageSize, pageNumber, searchQuery, visitorCardId);

            return Ok(summaries);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationDTO dto)
        {
            var reservationId = await _reservationService.CreateReservation(dto);

            if (reservationId != -1)
            {
                return CreatedAtAction(nameof(GetReservationById), new { id = reservationId }, dto);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id:int}/decline")]
        public async Task<IActionResult> DeclineReservation(int id)
        {
            await _reservationService.DeclineReservation(id);
            return Ok();
        }

        [HttpPut("{id:int}/accept")]
        public async Task<IActionResult> AcceptReservation(int id)
        {
            await _reservationService.AcceptReservation(id); 
            return Ok();
        }

        [HttpPut("{id:int}/checkout")]
        public async Task<IActionResult> CkeckOutReservation(int id)
        {
            await _reservationService.CheckOutReservation(id);
            return Ok();
        }
    }
}
