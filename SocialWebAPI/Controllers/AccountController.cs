using Microsoft.AspNetCore.Mvc;
using SocialWebAPI.Db;
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
        public async Task<ActionResult<AppUser>> Register(string username, string password)
        {
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key
            };

            _context.AppUsers.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
