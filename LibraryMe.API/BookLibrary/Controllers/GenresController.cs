using BookLibrary.BAL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : Controller
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            var genres = await _genreService.GetGenres();

            return Ok(genres);
        }
    }
}
