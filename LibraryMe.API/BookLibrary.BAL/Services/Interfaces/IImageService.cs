using Microsoft.AspNetCore.Http;

namespace BookLibrary.BAL.Services.Interfaces
{
    public interface IImageService
    {
        Task<Guid> CreateImageAsync(IFormFile file);
        Task<Guid?> GetImageIdByLinkAsync(string link);
    }
}
