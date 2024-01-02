using BookLibrary.BAL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController:ControllerBase
    {
        private readonly IImageService _imageService;

        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateImageAsync([FromForm] IFormFile file)
        {
                var imageId = await _imageService.CreateImageAsync(file);
                return Ok(imageId);
        }

        [HttpGet("{link}")]
        public async Task<IActionResult> GetImageIdByLink(string link)
        {
            var imageId = await _imageService.GetImageIdByLinkAsync(link);

            if (imageId == null) return NotFound();

            return Ok(imageId);
        }
    }
}
