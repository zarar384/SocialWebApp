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
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddScoped<LogUserActivity>();
            services.AddScoped<ILikesRepository, LikesRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            
            return services;
        }
    }
}
