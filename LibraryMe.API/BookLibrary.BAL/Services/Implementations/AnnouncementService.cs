using BookLibrary.BAL.Services.Interfaces;
using BookLibrary.DAL.Models.DTO;
using BookLibrary.DAL.Repositories.Interfaces;

namespace BookLibrary.BAL.Services.Implementations
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository _announcementRepo;

        public AnnouncementService(IAnnouncementRepository announcementService)
        {
            _announcementRepo = announcementService;
        }

        public async Task<AnnouncementDTO> GetAnnouncementById(Guid id)
        {
            return await _announcementRepo.GetAnnouncementById(id);
        }
        public async Task<List<AnnouncementDTO>> GetAnnouncementsAsync(int pageSize = 5, int pageNumber = 1)
        {
            return await _announcementRepo.GetAnnouncementsAsync(pageSize, pageNumber);
        }
        public async Task<List<AnnouncementDTO>> GetAnnouncementSummariesAsync(int pageSize = 5, int pageNumber = 1)
        {
            return await _announcementRepo.GetAnnouncementSummariesAsync(pageSize, pageNumber);
        }
        public async Task<Guid> CreateAnnouncement(AnnouncementDTO dto)
        {
            return await _announcementRepo.CreateAnnouncement(dto);
        }

        public async Task<AnnouncementDTO?> UpdateAnnouncement(UpdateAnnouncementDTO dto, Guid id)
        {
            return await _announcementRepo.UpdateAnnouncement(dto, id);
        }

        public async Task<AnnouncementDTO?> DeleteAnnouncement(Guid id)
        {
            return await _announcementRepo.DeleteAnnouncement(id);
        }
    }

}
