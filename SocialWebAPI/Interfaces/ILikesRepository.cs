using SocialWebAPI.Entities;
using SocialWebAPI.Helpers;

namespace SocialWebAPI;

public interface ILikesRepository
{
    Task<UserLike> GetUserLike(int sourceUserId, int targetUserId);
    Task<AppUser> GetUserWithLikes(int userId);
    Task<PageList<LikeDto>> GetUserLikes(LikesParams likesParams);
}
