using SocialWebAPI.DTOs;
using SocialWebAPI.Entities;
using SocialWebAPI.Helpers;

namespace SocialWebAPI.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        // Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetByIdAsync(int id);
        Task<AppUser> GetByUserNameAsync(string userName);
        Task<PageList<MemberDto>> GetMembersAsync(UserParams userParams);
        Task<MemberDto> GetMemberByNameAsync(string memberName);
        Task<string> GetUserGender(string username);
    }
}
