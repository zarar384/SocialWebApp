using Microsoft.EntityFrameworkCore;
using SocialWebAPI.Entities;

namespace SocialWebAPI.Db
{
    public class AppDbContext : DbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }

        #region test Db
        //public AppDbContext()
        //{
        //    Database.EnsureCreated();
        //}
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=LAPTOP-7UKLE6TU; Database = SocialWebAPI;Trusted_Connection=True; MultipleActiveResultSets= True;");
        //}
        #endregion
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
