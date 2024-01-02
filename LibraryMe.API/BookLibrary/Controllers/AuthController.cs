using BookLibrary.BAL.Services.Interfaces;
using BookLibrary.DAL.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO dto)
        {
            var result = await _authService.Register(dto);

            if (result!=null) return Ok();

            return BadRequest("Couldn't register user");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO dto)
        {
            var result = await _authService.Login(dto);

            if (result!=null) return Ok(result);

            return BadRequest("Couldn't login user");
        }
    }
}
