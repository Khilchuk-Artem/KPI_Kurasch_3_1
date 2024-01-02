using BookLibrary.BAL.Services.Interfaces;
using BookLibrary.DAL.Models.DTO;
using BookLibrary.DAL.Repositories.Interfaces;

namespace BookLibrary.BAL.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepo;

        public AuthorService(IAuthorRepository authorRepo)
        {
            _authorRepo = authorRepo;
        }

        public async Task<AuthorDTO?> GetAuthorById(Guid id)
        {
            return await _authorRepo.GetAuthorById(id);
        }

        public async Task<List<AuthorDTO>> GetAuthorsAsync(int pageSize = 5, int pageNumber = 1)
        {
            return await _authorRepo.GetAuthorsAsync(pageSize, pageNumber);
        }

        public async Task<List<AuthorSummaryDTO>> GetAuthorSummariesAsync(int pageSize = 5, int pageNumber = 1, string searchQuery = "")
        {
            return await _authorRepo.GetAuthorSummariesAsync(pageSize, pageNumber, searchQuery);
        }

        public async Task<List<AuthorLinkDTO>> GetAuthorLinksAsync()
        {
            return await _authorRepo.GetAuthorLinksAsync();
        }

        public async Task<Guid> CreateAuthor(CreateAuthorDTO dto)
        {
            return await _authorRepo.CreateAuthor(dto);
        }

        public async Task<AuthorDTO?> UpdateAuthor(CreateAuthorDTO dto, Guid id)
        {
            return await _authorRepo.UpdateAuthor(dto, id);
        }

        public async Task<AuthorDTO?> DeleteAuthor(Guid id)
        {
            return await _authorRepo.DeleteAuthor(id);
        }
    }
}
