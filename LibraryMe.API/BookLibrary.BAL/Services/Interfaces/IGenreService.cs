using BookLibrary.DAL.Models.DTO;

namespace BookLibrary.BAL.Services.Interfaces
{
    public interface IGenreService
    {
        Task<List<GenreDTO>> GetGenres();
    }
}
