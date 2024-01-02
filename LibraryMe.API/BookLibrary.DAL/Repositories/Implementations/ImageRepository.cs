using BookLibrary.DAL.Data;
using BookLibrary.DAL.Models.Domain;
using BookLibrary.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.DAL.Repositories.Implementations
{
    public class ImageRepository : IImageRepository
    {
        private readonly BookLibraryDbContext _dbContext;

        public ImageRepository(BookLibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> CreateImageAsync(Image image)
        {
            await _dbContext.Images.AddAsync(image);
            await _dbContext.SaveChangesAsync();
            return image.Id;
        }

        public async Task<Guid?> GetImageIdByLinkAsync(string link)
        {
            var correctLink = link.Replace("%2F", "/");
            var image = await _dbContext.Images.Where(i => i.Url == correctLink).FirstOrDefaultAsync();
            return image?.Id;
        }
    }
}
