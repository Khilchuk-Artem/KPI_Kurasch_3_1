using BookLibrary.Data;
using BookLibrary.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : Controller
    {
        private readonly BookLibraryDbContext _dbContext; // Add your DbContext instance

        public GenresController(BookLibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetGenres()
        {
            var genresFromDb = _dbContext.Genres.ToList();

            var genreDTOs = genresFromDb.Select(genre => new GenreDTO
            {
                Id = genre.Id,
                Name = genre.Name
            }).ToList();

            return Ok(genreDTOs);
        }
    }
}
