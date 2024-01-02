using BookLibrary.Models.Domain;

namespace BookLibrary.Services
{
    public class ImageUploaderService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ImageUploaderService(IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
        {
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task UploadImage(IFormFile file,Image img)
        {
            var localPath = Path.Combine(_environment.ContentRootPath, "Images", $"{img.FileName}");
            using var stream = new FileStream(localPath,FileMode.Create);
            await file.CopyToAsync(stream);

            var httpReguest = _httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httpReguest.Scheme}://{httpReguest.Host}{httpReguest.PathBase}/Images/{img.FileName}";

            img.Url = urlPath;
        }
    }
}
