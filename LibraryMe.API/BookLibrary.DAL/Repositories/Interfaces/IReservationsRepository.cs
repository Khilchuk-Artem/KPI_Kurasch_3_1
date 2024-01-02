using BookLibrary.DAL.Models.DTO;

namespace BookLibrary.DAL.Repositories.Interfaces
{
    // Interfaces
    public interface IReservationRepository
    {
        Task<ReservationDTO> GetReservationById(int id);
        Task<List<ReservationSummaryDTO>> GetReservationSummaries(int pageSize, int pageNumber, string searchQuery, int? visitorCardId);
        Task<int> CreateReservation(CreateReservationDTO dto);
        Task DeclineReservation(int id);
        Task AcceptReservation(int id);
        Task CheckOutReservation(int id);
    }
}
