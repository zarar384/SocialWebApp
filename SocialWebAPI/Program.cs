namespace API
{
    public class Program
    {

        public static void Main(string[] args)
        {
            #region startedData
            //using (AppDbContext db = new AppDbContext())
            //{
            //    AppUser user1 = new AppUser { UserName = "Bob" };
            //    AppUser user2 = new AppUser { UserName = "Sam" };
            //    AppUser user3 = new AppUser { UserName = "Nick" };
            //    db.AppUsers.Add(user1);
            //    db.AppUsers.Add(user2);
            //    db.AppUsers.Add(user3);
            //    db.SaveChanges();
            //}
            #endregion

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
