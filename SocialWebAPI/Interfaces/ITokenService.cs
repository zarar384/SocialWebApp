using SocialWebAPI.Entities;

namespace SocialWebAPI.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
