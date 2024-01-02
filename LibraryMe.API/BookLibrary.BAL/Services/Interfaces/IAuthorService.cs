using BookLibrary.DAL.Models.DTO;

namespace BookLibrary.BAL.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<Guid> CreateAuthor(CreateAuthorDTO dto);
        Task<AuthorDTO?> DeleteAuthor(Guid id);
        Task<AuthorDTO?> GetAuthorById(Guid id);
        Task<List<AuthorLinkDTO>> GetAuthorLinksAsync();
        Task<List<AuthorDTO>> GetAuthorsAsync(int pageSize = 5, int pageNumber = 1);
        Task<List<AuthorSummaryDTO>> GetAuthorSummariesAsync(int pageSize = 5, int pageNumber = 1, string searchQuery = "");
        Task<AuthorDTO?> UpdateAuthor(CreateAuthorDTO dto, Guid id);
    }
}
