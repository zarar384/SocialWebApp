using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialWebAPI.Db;
using SocialWebAPI.DTOs;
using SocialWebAPI.Entities;
using System.Security.Cryptography;
using System.Text;

namespace SocialWebAPI.Controllers
{
    public class AccountController : BaseAPIController
    {
        public AppDbContext _context { get; }

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");

            using var hmac = new HMACSHA512();
 
            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.AppUsers.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUser>> Login(RegisterDto loginDto)
        {
            var user =  _context.AppUsers.Where(x => x.UserName == loginDto.Username).FirstOrDefault();

            if (user == null) return Unauthorized("Invalid username");
            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }
            return Ok(user);
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.AppUsers.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}
