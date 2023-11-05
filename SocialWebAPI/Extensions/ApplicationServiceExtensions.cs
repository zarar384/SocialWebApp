using Microsoft.EntityFrameworkCore;
using SocialWebAPI.Db;
using SocialWebAPI.Helpers;
using SocialWebAPI.Interfaces;
using SocialWebAPI.Services;

namespace SocialWebAPI.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContextPool<AppDbContext>(option =>
                option.UseSqlServer(config.GetConnectionString("DefaultConnection"),
                settings => settings.EnableRetryOnFailure().CommandTimeout(60)));
            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddScoped<LogUserActivity>();
            services.AddSignalR();
            services.AddSingleton<PresenceTracker>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
