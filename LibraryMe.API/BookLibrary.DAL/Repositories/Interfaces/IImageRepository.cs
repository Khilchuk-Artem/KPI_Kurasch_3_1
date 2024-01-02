using BookLibrary.DAL.Models.Domain;

namespace BookLibrary.DAL.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<Guid> CreateImageAsync(Image image);
        Task<Guid?> GetImageIdByLinkAsync(string link);
    }
}
