using AutoMapper;
using BookLibrary.DAL.Data;
using BookLibrary.DAL.Models.Domain;
using BookLibrary.DAL.Models.DTO;
using BookLibrary.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.DAL.Repositories.Implementations
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly BookLibraryDbContext _dbContext;
        private readonly IMapper _mapper;

        public ReservationRepository(BookLibraryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ReservationDTO> GetReservationById(int id)
        {
            var reservation = await _dbContext.Reservations
                .Include(r => r.Books)
                .ThenInclude(b => b.Image)
                .Include(r => r.ReservationStatus)
                .FirstOrDefaultAsync(r => r.Id == id);

            return reservation != null ? _mapper.Map<ReservationDTO>(reservation) : null;
        }

        public async Task<List<ReservationSummaryDTO>> GetReservationSummaries(int pageSize, int pageNumber, string searchQuery, int? visitorCardId)
        {
            var query = _dbContext.Reservations
                .Where(r => r.Id.ToString().Contains(searchQuery)).AsQueryable();
            if (visitorCardId.HasValue)
            {
                query = query.Where(r => r.Reservator.Id == visitorCardId.Value);
            }
            var results = await query
                .OrderByDescending(r => r.DateCreated)
                .Include(r => r.ReservationStatus)
                .Include(r => r.Reservator)
                .OrderBy(r => r.DateCreated)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(r => _mapper.Map<ReservationSummaryDTO>(r))
                .ToListAsync();

            return results;
        }

        public async Task<int> CreateReservation(CreateReservationDTO dto)
        {
            var books = await _dbContext.Books.Where(b => !b.IsDeleted && dto.BookIds.Contains(b.Id)).ToListAsync();
            if (books.Count != dto.BookIds.Count()) throw new InvalidOperationException("Invalid book IDs");

            var reservation = new Reservation()
            {
                DateCreated = DateTime.Now,
                ReservatorId = dto.ReservatorId,
                ReservationStatusId = Guid.Parse("929B2083-C7B5-4D8C-B216-F02B0DC65AF7"),
                Books = books
            };

            await _dbContext.Reservations.AddAsync(reservation);
            await _dbContext.SaveChangesAsync();

            return reservation.Id;
        }

        public async Task DeclineReservation(int id)
        {
            var reservation = await _dbContext.Reservations
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
                throw new InvalidOperationException("Reservation not found");
            if (reservation.ReservationStatusId != Guid.Parse("929B2083-C7B5-4D8C-B216-F02B0DC65AF7"))
                throw new InvalidOperationException("Invalid reservation status");

            reservation.ReservationStatusId = Guid.Parse("5b0b6de5-7db3-4fb1-9173-8a1f4c2ff9c9");
            _dbContext.Reservations.Update(reservation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AcceptReservation(int id)
        {
            var reservation = await _dbContext.Reservations
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
                throw new InvalidOperationException("Reservation not found");
            if (reservation.ReservationStatusId != Guid.Parse("929B2083-C7B5-4D8C-B216-F02B0DC65AF7"))
                throw new InvalidOperationException("Invalid reservation status");

            reservation.ReservationStatusId = Guid.Parse("70B5342F-F380-47CF-B9D1-5E3F42A15FF0");
            reservation.DateAccepted = DateTime.Now;
            _dbContext.Reservations.Update(reservation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CheckOutReservation(int id)
        {
            var reservation = await _dbContext.Reservations
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
                throw new InvalidOperationException("Reservation not found");
            if (reservation.ReservationStatusId != Guid.Parse("70B5342F-F380-47CF-B9D1-5E3F42A15FF0"))
                throw new InvalidOperationException("Invalid reservation status");

            reservation.ReservationStatusId = Guid.Parse("CC7951BD-8930-48C0-B7CE-AA60274C610E");
            reservation.DateCheckedOut = DateTime.Now;
            _dbContext.Reservations.Update(reservation);
            await _dbContext.SaveChangesAsync();
        }
    }

}
