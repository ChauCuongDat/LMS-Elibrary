using LMS_Elibrary.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LMS_Elibrary.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<UserDto> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<UserDto> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<bool> Register(UserDto request, string password)
        {

            var userDto = new UserDto
            {
                UserName = request.UserName
            };
            var result = await _userManager.CreateAsync(userDto, password);

            return result.Succeeded;
        }


        public async Task<string> Login(UserDto request, string password)
        {
            var identityUser = await _userManager.FindByNameAsync(request.UserName);
            if(identityUser == null)
            {
                return "false";
            }
            if (!await _userManager.CheckPasswordAsync(identityUser, password)) 
            { 
                return "false";
            }
            string token = await CreateToken(request);
            return token;
        }

        private async Task<string> CreateToken(UserDto user)
        {
            var roleList = await _userManager.GetRolesAsync(user);
            var claimRoleList = roleList.Select(i => new Claim(ClaimTypes.Role,i)).ToList();

            List<Claim> claim = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            claim.AddRange(claimRoleList);

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSetting:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claim,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: cred
                );

            string jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}
