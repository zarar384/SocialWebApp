using Microsoft.EntityFrameworkCore;
using SocialWebAPI.Db;
using SocialWebAPI.Interfaces;
using SocialWebAPI.Services;

namespace SocialWebAPI.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddDbContext<AppDbContext>(option =>
            option.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
