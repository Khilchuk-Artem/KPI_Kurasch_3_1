using BookLibrary.DAL.Models.DTO;

namespace BookLibrary.DAL.Repositories.Interfaces
{
    public interface IVisitorCardRepository
    {
        Task<VisitorCardDTO> GetVisitorCardByIdAsync(int id);
        Task<List<VisitorCardDTO>> GetVisitorCardsAsync();
        Task<List<VisitorCardShortcutDTO>> GetVisitorCardShortcutsAsync(int pageSize, int pageNumber, string query);
        Task<int> CreateVisitorCardAsync(CreateVisitorCardDTO dto);
        Task<VisitorCardDTO?> UpdateVisitorCardAsync(CreateVisitorCardDTO dto, int id);
        Task<VisitorCardDTO?> DeleteVisitorCardAsync(int id);
    }
}
