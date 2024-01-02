using BookLibrary.DAL.Models.DTO;

namespace BookLibrary.DAL.Repositories.Interfaces
{
    public interface IAnnouncementRepository
    {
        Task<Guid> CreateAnnouncement(AnnouncementDTO dto);
        Task<AnnouncementDTO?> DeleteAnnouncement(Guid id);
        Task<AnnouncementDTO?> GetAnnouncementById(Guid id);
        Task<List<AnnouncementDTO>> GetAnnouncementsAsync(int pageSize = 5, int pageNumber = 1);
        Task<List<AnnouncementDTO>> GetAnnouncementSummariesAsync(int pageSize = 5, int pageNumber = 1);
        Task<AnnouncementDTO?> UpdateAnnouncement(UpdateAnnouncementDTO dto, Guid id);
    }
}
