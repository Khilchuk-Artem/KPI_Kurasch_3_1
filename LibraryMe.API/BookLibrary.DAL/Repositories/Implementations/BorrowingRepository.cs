using AutoMapper;
using BookLibrary.DAL.Data;
using BookLibrary.DAL.Models.Domain;
using BookLibrary.DAL.Models.DTO;
using BookLibrary.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace BookLibrary.DAL.Repositories.Implementations
{
    public class BorrowingRepository : IBorrowingRepository
    {
        private readonly BookLibraryDbContext _dbContext;
        private readonly IMapper _mapper;

        public BorrowingRepository(BookLibraryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<BorrowingDTO?> GetBorrowingById(Guid id)
        {
            var borrowing = await _dbContext.Borrowings
                .Include(b => b.Books).ThenInclude(b => b.Image)
                .Include(b => b.Books).ThenInclude(book => book.Authors)
                .Include(b => b.VisitorsCard)
                .Include(b => b.BorrowingStatus)
                .FirstOrDefaultAsync(b => b.Id == id);

            return borrowing != null ? _mapper.Map<BorrowingDTO>(borrowing) : null;
        }

        public async Task<List<BorrowingSummaryDTO>> GetBorrowingSummariesAsync(int pageSize = 10, int pageNumber = 1, int? borrowerCardId = null, string borrowerName = null, bool hideReturned = false, DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _dbContext.Borrowings
                .Include(b => b.VisitorsCard)
                .Include(b => b.BorrowingStatus)
                .OrderByDescending(b => b.DateCreated).AsQueryable();

            if (borrowerCardId.HasValue)
            {
                query = query.Where(b => b.VisitorsCardId == borrowerCardId);
            }
            if (hideReturned)
            {
                query = query.Where(b => b.BorrowingStatusId != Guid.Parse("76C30481-34B8-493E-857E-75622551A448"));
            }
            if (borrowerName != null)
            {
                query = query.Where(b => (b.VisitorsCard.Surname + " " + b.VisitorsCard.Name + " " + b.VisitorsCard.Patronymic).Contains(borrowerName));
            }
            if (startDate != null)
            {
                query = query.Where(b => b.DateCreated >= startDate.Value);
            }
            if (endDate != null)
            {
                query = query.Where(b => b.DateCreated <= endDate.Value);
            }

            var results = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(b => new BorrowingSummaryDTO
                {
                    BorrowingId = b.Id,
                    CreatedDate = DateOnly.FromDateTime(b.DateCreated.Date),
                    Status = b.BorrowingStatus.Name,
                    Creator = $"{b.VisitorsCard.Name} {b.VisitorsCard.Surname}"
                })
                .ToListAsync();

            return results;
        }

        public async Task<Guid> CreateBorrowing(CreateBorrowingDTO dto)
        {
            var books = await _dbContext.Books.Where(b => !b.IsDeleted && dto.BookIds.Contains(b.Id)).ToListAsync();
            if (books.Count != dto.BookIds.Count()) return Guid.Empty;

            var borrowing = new Borrowing()
            {
                DateCreated = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14),
                VisitorsCardId = dto.BorrowerId,
                BorrowingStatusId = Guid.Parse("73bb3243-c71c-4f1b-ba1f-f4fc56b5dee2"),
                Books = books
            };

            await _dbContext.Borrowings.AddAsync(borrowing);
            await _dbContext.SaveChangesAsync();

            return borrowing.Id;
        }

        public async Task<bool> ReturnBorrowing(Guid id)
        {
            var borrowing = await _dbContext.Borrowings.FindAsync(id);
            if (borrowing == null) return false;

            borrowing.BorrowingStatusId = Guid.Parse("76C30481-34B8-493E-857E-75622551A448");
            _dbContext.Update(borrowing);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<BorrowingDTO?> DeleteBorrowing(Guid id)
        {
            var borrowing = await _dbContext.Borrowings
                .Include(b => b.Books)
                .Include(b => b.VisitorsCard)
                .Include(b => b.BorrowingStatus)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (borrowing == null)
                return null;

            _dbContext.Borrowings.Remove(borrowing);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<BorrowingDTO>(borrowing);
        }
    }
}
