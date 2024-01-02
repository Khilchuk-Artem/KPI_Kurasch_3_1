using BookLibrary.DAL.Models.DTO;

namespace BookLibrary.DAL.Repositories.Interfaces
{
    public interface IGenreRepository
    {
        Task<List<GenreDTO>> GetGenres();
    }
}
