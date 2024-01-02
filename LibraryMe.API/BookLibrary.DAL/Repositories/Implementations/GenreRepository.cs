using AutoMapper;
using BookLibrary.DAL.Data;
using BookLibrary.DAL.Models.DTO;
using BookLibrary.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.DAL.Repositories.Implementations
{
    public class GenreRepository : IGenreRepository
    {
        private readonly BookLibraryDbContext _dbContext;
        private readonly IMapper _mapper;

        public GenreRepository(BookLibraryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<GenreDTO>> GetGenres()
        {
            var genresFromDb = await _dbContext.Genres.ToListAsync();

            return genresFromDb.Select(genre => _mapper.Map<GenreDTO>(genre)).ToList();
        }
    }
}
