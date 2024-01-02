using BookLibrary.Data;
using BookLibrary.Models.Domain;
using BookLibrary.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController:ControllerBase
    {
        private readonly BookLibraryDbContext _dbContext;
        private readonly ImageUploaderService _imageUploaderService;
        public ImagesController(BookLibraryDbContext dbContext, ImageUploaderService imageUploaderService)
        {
            _dbContext = dbContext;
            _imageUploaderService = imageUploaderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateImageAsync([FromForm] IFormFile file)
        {
            ValidateFileUpload(file);
            if(ModelState.IsValid)
            {
                var image = new Image()
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = file.FileName,
                    DateUploaded = DateTime.Now,
                };
                await _imageUploaderService.UploadImage(file, image);

                await _dbContext.Images.AddAsync(image);
                await _dbContext.SaveChangesAsync();
                return Ok(image.Id);
            }
            return BadRequest();
        }
        [HttpGet("{link}")]
        public async Task<IActionResult> GetImageIdByLink(string link)
        {
            var correctLink = link.Replace("%2F", "/");

            var image= await _dbContext.Images.Where(i=>i.Url==correctLink).FirstOrDefaultAsync();
            if (image == null) return NotFound();
            return Ok(image.Id);
        }
        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg",".png" };

            if (!allowedExtension.Contains(Path.GetExtension(file.FileName).ToLower())) 
            {
                ModelState.AddModelError("file","Unsupported image format");
            }
            if (file.Length > 10485760)//10mb
            {
                ModelState.AddModelError("file", "File size can't be greater than 10mb");
            }
        }
    }
}
