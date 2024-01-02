using BookLibrary.BAL.Services.Implementations;
using BookLibrary.BAL.Services.Interfaces;
using BookLibrary.DAL.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSummaryController : ControllerBase
    {
        private readonly IUserSummaryService _userSummaryService;

        public UserSummaryController(IUserSummaryService userSummaryService)
        {
            _userSummaryService = userSummaryService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserProfileSummaryDTO>> GetUserSummaryById(string userId)
        {
            var summary = await _userSummaryService.GetUserSummaryById(userId);

            if (summary != null)
            {
                return Ok(summary);
            }

            return NotFound();
        }
        [HttpGet("shortcuts")]
        public async Task<ActionResult<UserProfileSummaryDTO>> GetUserSummaryShortcuts([FromQuery] int pageSize=10, [FromQuery] int pageNumber=1, [FromQuery] string query="")
        {
            var summary = await _userSummaryService.GetUserSummaryShortcut(pageSize,pageNumber,query);

            if (summary != null)
            {
                return Ok(summary);
            }

            return NotFound();
        }
        [HttpPut("{userId}")]
        public async Task<ActionResult<UserProfileSummaryDTO>> UpdateUserSummaryById(UpdateUserDTO updatedUserSummary, string userId)
        {
            var updatedSummary = await _userSummaryService.UpdateUserSummaryById(updatedUserSummary, userId);

            if (updatedSummary != null)
            {
                return Ok(updatedSummary);
            }

            return NotFound();
        }
    }
}
