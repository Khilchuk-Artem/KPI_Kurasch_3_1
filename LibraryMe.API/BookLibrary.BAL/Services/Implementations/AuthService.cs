﻿using BookLibrary.BAL.Services.Interfaces;
using BookLibrary.DAL.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookLibrary.BAL.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;
        public AuthService(UserManager<IdentityUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public async Task<string> CreateJWTToken(IdentityUser user, List<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("userId", user.Id),
                new Claim("visitorCardId",(await _userManager.GetClaimsAsync(user)).Where(c=>c.Type=="visitorCardId").FirstOrDefault().Value)
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<LoginResponseDTO> Login(LoginUserDTO loginUserDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginUserDTO.Email);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginUserDTO.Password);
                if (passwordCheck)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var card = (await _userManager.GetClaimsAsync(user)).Where(c => c.Type == "visitorCardId").FirstOrDefault().Value;
                    if (roles != null)
                    {
                        var response = new LoginResponseDTO()
                        {
                            Email = loginUserDTO.Email,
                            Name = user.UserName,
                            VisitorsCardId = card,
                            Roles = roles,
                            UserId = user.Id,
                            Token = await CreateJWTToken(user, roles.ToList())
                        };

                        return response;
                    }
                }
            }
            return null;
        }

        public async Task<IdentityResult> Register(RegisterUserDTO registerUserDTO)
        {
            var identityUser = new IdentityUser()
            {
                UserName = registerUserDTO.Name,
                Email = registerUserDTO.Email,
            };

            var identityResult = await _userManager.CreateAsync(identityUser, registerUserDTO.Password);
            if (identityResult.Succeeded)
            {
                if (registerUserDTO.Roles != null && registerUserDTO.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, registerUserDTO.Roles);
                    if (identityResult.Succeeded)
                    {
                        var claim = new Claim("visitorCardId", "");
                        var result = await _userManager.AddClaimAsync(identityUser, claim);

                        if (result.Succeeded)
                        {
                            return identityResult;
                        }
                    }
                }
            }
            return null;
        }
    }
}
