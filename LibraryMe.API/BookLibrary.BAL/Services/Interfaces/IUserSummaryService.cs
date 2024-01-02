using BookLibrary.DAL.Models.DTO;

namespace BookLibrary.BAL.Services.Interfaces
{
    public interface IUserSummaryService
    {
        Task<UserProfileSummaryDTO> GetUserSummaryById(string userId);
        Task<IEnumerable<UserSummaryShortcutDTO>> GetUserSummaryShortcut(int pageSize = 10, int pageNumber = 1, string seqrchQuery = "");
        Task<UserProfileSummaryDTO> UpdateUserSummaryById(UpdateUserDTO updatedUserSummary, string userId);
    }
}