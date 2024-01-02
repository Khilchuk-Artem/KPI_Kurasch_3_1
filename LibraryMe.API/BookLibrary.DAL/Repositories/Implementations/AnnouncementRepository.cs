using AutoMapper;
using BookLibrary.DAL.Data;
using BookLibrary.DAL.Models.Domain;
using BookLibrary.DAL.Models.DTO;
using BookLibrary.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.DAL.Repositories.Implementations
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly BookLibraryDbContext _dbContext;
        private readonly IMapper _mapper;

        public AnnouncementRepository(BookLibraryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<AnnouncementDTO?> GetAnnouncementById(Guid id)
        {
            var announcement = await _dbContext.Announcements.Where(a => !a.IsDeleted).FirstOrDefaultAsync(a => a.Id == id);

            if (announcement == null) return null;

            return _mapper.Map<AnnouncementDTO>(announcement);
        }
        public async Task<List<AnnouncementDTO>> GetAnnouncementsAsync(int pageSize = 5, int pageNumber = 1)
        {
            return await _dbContext.Announcements.Where(a => !a.IsDeleted).OrderByDescending(a => a.CreatedDate).Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(a => _mapper.Map<AnnouncementDTO>(a)).ToListAsync();
        }
        public async Task<List<AnnouncementDTO>> GetAnnouncementSummariesAsync(int pageSize = 5, int pageNumber = 1)
        {
            return await _dbContext.Announcements
                .Where(a => !a.IsDeleted)
                .OrderByDescending(a => a.CreatedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new AnnouncementDTO()
                {
                    Id = a.Id,
                    Title = a.Title,
                    Content = a.Content.Substring(0, Math.Min(a.Content.Length, 300)),
                    DateCreated = a.CreatedDate
                }).ToListAsync();
        }
        public async Task<Guid> CreateAnnouncement(AnnouncementDTO dto)
        {
            var announcement = _mapper.Map<Announcement>(dto);
            announcement.CreatedDate = DateTime.Now;
            await _dbContext.Announcements.AddAsync(announcement);
            await _dbContext.SaveChangesAsync();

            return announcement.Id;
        }

        public async Task<AnnouncementDTO?> UpdateAnnouncement(UpdateAnnouncementDTO dto, Guid id)
        {
            var announcement = await _dbContext.Announcements.Where(a => !a.IsDeleted).FirstOrDefaultAsync(a => a.Id == id);

            if (announcement == null) return null;

            announcement.Title = dto.Title;
            announcement.Content = dto.Content;

            _dbContext.Announcements.Update(announcement);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<AnnouncementDTO>(announcement);
        }

        public async Task<AnnouncementDTO?> DeleteAnnouncement(Guid id)
        {
            var announcement = await _dbContext.Announcements.FirstOrDefaultAsync(a => a.Id == id);

            if (announcement == null) return null;

            announcement.IsDeleted = true;

            _dbContext.Announcements.Update(announcement);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<AnnouncementDTO>(announcement);
        }
    }
}
