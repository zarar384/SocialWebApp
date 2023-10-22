using SocialWebAPI.Entities;

namespace SocialWebAPI.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
