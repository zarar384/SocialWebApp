using Microsoft.EntityFrameworkCore;
using SocialWebAPI.Entities;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace SocialWebAPI.Db
{
    public class Seed
    {
        public static async Task SeedUsers(AppDbContext context) 
        {
            if (await context.AppUsers.AnyAsync()) return;

            var userData = await File.ReadAllTextAsync("Db/UserSeedData.json");
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

            foreach(var user in users)
            {
                using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;

                context.AppUsers.Add(user);
            }

            await context.SaveChangesAsync();
        }
    }
}
