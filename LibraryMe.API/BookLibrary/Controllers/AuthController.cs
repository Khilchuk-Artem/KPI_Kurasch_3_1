using BookLibrary.Models.DTO;
using BookLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

namespace BookLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
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
