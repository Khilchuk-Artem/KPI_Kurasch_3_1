using BookLibrary.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace BookLibrary.Jobs
{
    [DisallowConcurrentExecution]
    public class ExpireBorrowingsJob : IJob
    {
        private readonly BookLibraryDbContext _dbContext;
        public ExpireBorrowingsJob(BookLibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var expiredBorrowings = await _dbContext.Borrowings
                .Where(b => b.BorrowingStatusId == Guid.Parse("73BB3243-C71C-4F1B-BA1F-F4FC56B5DEE2") && (b.DateCreated > b.DueDate))
                .ToListAsync();
            
            foreach(var b in expiredBorrowings)
            {
                b.BorrowingStatusId = Guid.Parse("f037329e-b42c-456a-bf8f-b79cbc786433");
                _dbContext.Borrowings.Update(b);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
