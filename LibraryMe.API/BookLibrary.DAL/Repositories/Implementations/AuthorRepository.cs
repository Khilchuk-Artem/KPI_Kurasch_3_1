using AutoMapper;
using BookLibrary.DAL.Data;
using BookLibrary.DAL.Models.Domain;
using BookLibrary.DAL.Models.DTO;
using BookLibrary.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.DAL.Repositories.Implementations
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookLibraryDbContext _dbContext;
        private readonly IMapper _mapper;

        public AuthorRepository(BookLibraryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<AuthorDTO?> GetAuthorById(Guid id)
        {
            var author = await _dbContext.Authors
                .Include(a => a.Image)
                .Include(a => a.Books).ThenInclude(b => b.Image)
                .Where(a => !a.IsDeleted)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (author == null) return null;

            return _mapper.Map<AuthorDTO>(author);
        }

        public async Task<List<AuthorDTO>> GetAuthorsAsync(int pageSize = 5, int pageNumber = 1)
        {
            return await _dbContext.Authors
                .Include(a => a.Image)
                .Where(a => !a.IsDeleted)
                .OrderBy(a => a.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(a => _mapper.Map<AuthorDTO>(a))
                .ToListAsync();
        }

        public async Task<List<AuthorSummaryDTO>> GetAuthorSummariesAsync(int pageSize = 5, int pageNumber = 1, string searchQuery = "")
        {
            return await _dbContext.Authors
                .Include(a => a.Image)
                .Where(a => !a.IsDeleted)
                .Where(a => (a.Name.ToLower() + " " + a.Patronymic.ToLower() + " " + a.Surname.ToLower()).Contains(searchQuery.ToLower()))
                .OrderBy(a => a.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(a => _mapper.Map<AuthorSummaryDTO>(a))
                .ToListAsync();
        }

        public async Task<List<AuthorLinkDTO>> GetAuthorLinksAsync()
        {
            return await _dbContext.Authors
                .Include(a => a.Image)
                .Where(a => !a.IsDeleted)
                .OrderBy(a => a.Name)
                .Select(a => _mapper.Map<AuthorLinkDTO>(a))
                .ToListAsync();
        }

        public async Task<Guid> CreateAuthor(CreateAuthorDTO dto)
        {
            var author = _mapper.Map<Author>(dto);

            await _dbContext.Authors.AddAsync(author);
            await _dbContext.SaveChangesAsync();

            return author.Id;
        }

        public async Task<AuthorDTO?> UpdateAuthor(CreateAuthorDTO dto, Guid id)
        {
            var author = await _dbContext.Authors.Include(a => a.Image).Where(a => !a.IsDeleted).FirstOrDefaultAsync(a => a.Id == id);

            if (author == null) return null;

            _mapper.Map(dto, author);

            _dbContext.Authors.Update(author);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<AuthorDTO>(author);
        }

        public async Task<AuthorDTO?> DeleteAuthor(Guid id)
        {
            var author = await _dbContext.Authors.Include(a => a.Books).Include(a => a.Image).FirstOrDefaultAsync(a => a.Id == id);

            if (author == null) return null;

            var booksWithoutAuthor = author.Books.Where(b => b.Authors.Where(a => !a.IsDeleted).Count() == 1).ToList();

            author.IsDeleted = true;

            foreach (var b in booksWithoutAuthor)
            {
                b.IsDeleted = true;
                _dbContext.Books.Update(b);
            }

            _dbContext.Authors.Update(author);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<AuthorDTO>(author);
        }
    }
}
