using BookLibrary.BAL.Services.Interfaces;
using BookLibrary.DAL.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookLibrary.BAL.Services.Implementations
{
    public class UserSummaryService : IUserSummaryService
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UserSummaryService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<UserProfileSummaryDTO> GetUserSummaryById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var visitorCardId = (await _userManager.GetClaimsAsync(user)).Where(c => c.Type == "visitorCardId").FirstOrDefault();
            if (user != null)
            {
                var summary = new UserProfileSummaryDTO()
                {
                    Name = user.UserName,
                    Email = user.Email,
                    VisitorCardId = visitorCardId != null ? visitorCardId.Value : null
                };
                return summary;
            }

            return null;
        }
        public async Task<IEnumerable<UserSummaryShortcutDTO>> GetUserSummaryShortcut(int pageSize = 10, int pageNumber = 1, string seqrchQuery = "")
        {
            var users = await _userManager.Users
                .Where(u => u.UserName.Contains(seqrchQuery))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).Select(u => new UserSummaryShortcutDTO()
                {
                    Id = u.Id,
                    Name = u.UserName
                }).ToListAsync();
            return users;
        }
        public async Task<UserProfileSummaryDTO> UpdateUserSummaryById(UpdateUserDTO updatedUserSummary, string userId)
        {
            IdentityUser user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                user.UserName = updatedUserSummary.Name;

                await _userManager.UpdateAsync(user);
                var existingClaim = (await _userManager.GetClaimsAsync(user))
                    .FirstOrDefault(c => c.Type == "visitorCardId");
                if (existingClaim != null)
                {
                    var result = await _userManager.RemoveClaimAsync(user, existingClaim);
                }
                var claim = new Claim("visitorCardId", updatedUserSummary.CardId);
                var resultAddClaim = await _userManager.AddClaimAsync(user, claim);
                if (resultAddClaim.Succeeded)
                {
                    var updatedSummary = new UserProfileSummaryDTO()
                    {
                        Name = user.UserName,
                        Email = user.Email,
                        VisitorCardId = (await _userManager.GetClaimsAsync(user)).Where(c => c.Type == "visitorCardId").FirstOrDefault().Value
                    };
                    return updatedSummary;
                }
                return null;
            }
            return null;
        }
    }
}
