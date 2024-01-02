using BookLibrary.BAL.Services.Interfaces;
using BookLibrary.DAL.Models.Domain;
using BookLibrary.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Hosting;

namespace BookLibrary.BAL.Services.Implementations
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepo;
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ImageService(IImageRepository imageRepo, IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
        {
            _imageRepo = imageRepo;
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Guid> CreateImageAsync(IFormFile file)
        {

            if (!ValidateFileUpload(file))
            {
                throw new ValidationException("Invalid file upload.");
            }

            var image = new Image()
            {
                FileExtension = Path.GetExtension(file.FileName).ToLower(),
                FileName = file.FileName,
                DateUploaded = DateTime.Now,
            };

            var localPath = Path.Combine(_environment.ContentRootPath, "Images", $"{image.FileName}");

            using (var stream = new FileStream(localPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var httpRequest = _httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{image.FileName}";

            image.Url = urlPath;

            return await _imageRepo.CreateImageAsync(image);
        }

        public async Task<Guid?> GetImageIdByLinkAsync(string link)
        {
            return await _imageRepo.GetImageIdByLinkAsync(link);
        }

        private bool ValidateFileUpload(IFormFile file)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtension.Contains(Path.GetExtension(file.FileName).ToLower()))
            { 
                return false;
            }

            if (file.Length > 10485760) // 10mb
            {
                return false;
            }
            return true;
        }
    }
}
