using BookLibrary.BAL.Services.Interfaces;
using BookLibrary.DAL.Repositories.Interfaces;
using BookLibrary.DAL.Models.DTO;

namespace BookLibrary.BAL.Services.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<ReservationDTO> GetReservationById(int id)
        {
            return await _reservationRepository.GetReservationById(id);
        }

        public async Task<List<ReservationSummaryDTO>> GetReservationSummaries(int pageSize, int pageNumber, string searchQuery, int? visitorCardId)
        {
            return await _reservationRepository.GetReservationSummaries(pageSize, pageNumber, searchQuery, visitorCardId);
        }

        public async Task<int> CreateReservation(CreateReservationDTO dto)
        {
            return await _reservationRepository.CreateReservation(dto);
        }

        public async Task DeclineReservation(int id)
        {
            await _reservationRepository.DeclineReservation(id);
        }

        public async Task AcceptReservation(int id)
        {
            await _reservationRepository.AcceptReservation(id);
        }

        public async Task CheckOutReservation(int id)
        {
            await _reservationRepository.CheckOutReservation(id);
        }
    }
}