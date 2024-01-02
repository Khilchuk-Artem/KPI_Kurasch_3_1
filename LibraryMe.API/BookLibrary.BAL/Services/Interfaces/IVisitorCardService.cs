using BookLibrary.DAL.Models.DTO;

namespace BookLibrary.BAL.Services.Interfaces
{
    public interface IVisitorCardService
    {
        Task<VisitorCardDTO> GetVisitorCardByIdAsync(int id);
        Task<List<VisitorCardDTO>> GetVisitorCardsAsync();
        Task<List<VisitorCardShortcutDTO>> GetVisitorCardShortcutsAsync(int pageSize, int pageNumber, string query);
        Task<int> CreateVisitorCardAsync(CreateVisitorCardDTO dto);
        Task<VisitorCardDTO?> UpdateVisitorCardAsync(CreateVisitorCardDTO dto, int id);
        Task<VisitorCardDTO?> DeleteVisitorCardAsync(int id);
    }
}
