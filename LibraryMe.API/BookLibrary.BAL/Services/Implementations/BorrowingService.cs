using BookLibrary.BAL.Services.Interfaces;
using BookLibrary.DAL.Repositories.Interfaces;
using BookLibrary.DAL.Models.DTO;

namespace BookLibrary.BAL.Services.Implementations
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IBorrowingRepository _borrowingRepo;

        public BorrowingService(IBorrowingRepository borrowingRepo)
        {
            _borrowingRepo = borrowingRepo;
        }

        public async Task<BorrowingDTO?> GetBorrowingById(Guid id)
        {
            return await _borrowingRepo.GetBorrowingById(id);
        }

        public async Task<List<BorrowingSummaryDTO>> GetBorrowingSummariesAsync(int pageSize = 10, int pageNumber = 1, int? borrowerCardId = null, string borrowerName = null, bool hideReturned = false, DateTime? startDate = null, DateTime? endDate = null)
        {
            return await _borrowingRepo.GetBorrowingSummariesAsync(pageSize, pageNumber, borrowerCardId, borrowerName, hideReturned, startDate, endDate);
        }

        public async Task<Guid> CreateBorrowing(CreateBorrowingDTO dto)
        {
            return await _borrowingRepo.CreateBorrowing(dto);
        }

        public async Task<bool> ReturnBorrowing(Guid id)
        {
            return await _borrowingRepo.ReturnBorrowing(id);
        }

        public async Task<BorrowingDTO?> DeleteBorrowing(Guid id)
        {
            return await _borrowingRepo.DeleteBorrowing(id);
        }
    }
}
