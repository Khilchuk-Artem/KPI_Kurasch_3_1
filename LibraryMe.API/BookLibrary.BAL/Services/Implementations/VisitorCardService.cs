using BookLibrary.BAL.Services.Interfaces;
using BookLibrary.DAL.Repositories.Interfaces;
using BookLibrary.DAL.Models.DTO;

namespace BookLibrary.BAL.Services.Implementations
{
    public class VisitorCardService : IVisitorCardService
    {
        private readonly IVisitorCardRepository _visitorCardRepo;

        public VisitorCardService(IVisitorCardRepository visitorCardRepo)
        {
            _visitorCardRepo = visitorCardRepo;
        }

        public async Task<VisitorCardDTO> GetVisitorCardByIdAsync(int id)
        {
            return await _visitorCardRepo.GetVisitorCardByIdAsync(id);
        }

        public async Task<List<VisitorCardDTO>> GetVisitorCardsAsync()
        {
            return await _visitorCardRepo.GetVisitorCardsAsync();
        }

        public async Task<List<VisitorCardShortcutDTO>> GetVisitorCardShortcutsAsync(int pageSize, int pageNumber, string query)
        {
            return await _visitorCardRepo.GetVisitorCardShortcutsAsync(pageSize, pageNumber, query);
        }

        public async Task<int> CreateVisitorCardAsync(CreateVisitorCardDTO dto)
        {
            return await _visitorCardRepo.CreateVisitorCardAsync(dto);
        }

        public async Task<VisitorCardDTO?> UpdateVisitorCardAsync(CreateVisitorCardDTO dto, int id)
        {
            return await _visitorCardRepo.UpdateVisitorCardAsync(dto, id);
        }

        public async Task<VisitorCardDTO?> DeleteVisitorCardAsync(int id)
        {
            return await _visitorCardRepo.DeleteVisitorCardAsync(id);
        }
    }
}
