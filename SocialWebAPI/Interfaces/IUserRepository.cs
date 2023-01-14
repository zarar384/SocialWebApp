using SocialWebAPI.Entities;
using System.Threading.Tasks;

namespace SocialWebAPI.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetByIdAsync(int id);
        Task<AppUser> GetByUserNameAsync(string userName);
    }
}
