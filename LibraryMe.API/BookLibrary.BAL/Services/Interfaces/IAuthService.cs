using BookLibrary.DAL.Models.DTO;
using Microsoft.AspNetCore.Identity;

namespace BookLibrary.BAL.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> CreateJWTToken(IdentityUser user, List<string> roles);
        Task<LoginResponseDTO> Login(LoginUserDTO loginUserDTO);
        Task<IdentityResult> Register(RegisterUserDTO registerUserDTO);
    }
}