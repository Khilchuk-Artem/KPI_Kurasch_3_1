using BookLibrary.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace BookLibrary.Jobs
{
    public class ExpireReservationsJob : IJob
    {
        private readonly BookLibraryDbContext _dbContext;
        public ExpireReservationsJob(BookLibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var expiredReservations = await _dbContext.Reservations
                            .Where(r => r.ReservationStatusId == Guid.Parse("70b5342f-f380-47cf-b9d1-5e3f42a15ff0") && (r.DateAccepted.Value > r.DateAccepted.Value.AddHours(8)))
                            .ToListAsync();

            foreach (var r in expiredReservations)
            {
                r.ReservationStatusId = Guid.Parse("865a254e-5f32-44b1-aa2f-add87443bfb0");
                _dbContext.Reservations.Update(r);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
