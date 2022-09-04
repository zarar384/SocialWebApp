using Microsoft.EntityFrameworkCore;
using SocialWebAPI.Entities;

namespace SocialWebAPI.Db
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
