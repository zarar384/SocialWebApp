using SocialWebAPI.Db;
using SocialWebAPI.Entities;
using SocialWebAPI.Helpers;
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
        Task<PageList<MemberDto>> GetMembersAsync(UserParams userParams);
        Task<MemberDto> GetMemberByNameAsync(string memberName);
    }
}
