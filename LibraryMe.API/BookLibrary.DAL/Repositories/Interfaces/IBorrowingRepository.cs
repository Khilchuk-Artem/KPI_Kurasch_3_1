using BookLibrary.DAL.Models.DTO;

namespace BookLibrary.DAL.Repositories.Interfaces
{
    public interface IBorrowingRepository
    {
        Task<BorrowingDTO?> GetBorrowingById(Guid id);
        Task<List<BorrowingSummaryDTO>> GetBorrowingSummariesAsync(int pageSize = 10, int pageNumber = 1, int? borrowerCardId = null, string borrowerName = null, bool hideReturned = false, DateTime? startDate = null, DateTime? endDate = null);
        Task<Guid> CreateBorrowing(CreateBorrowingDTO dto);
        Task<bool> ReturnBorrowing(Guid id);
        Task<BorrowingDTO?> DeleteBorrowing(Guid id);
    }
}
