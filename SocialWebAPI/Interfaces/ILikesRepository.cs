using SocialWebAPI.Entities;

namespace SocialWebAPI;

public interface ILikesRepository
{
    Task<UserLike> GetUserLike(int sourceUserId, int targetUserId);
    Task<AppUser> GetUserWithLikes(int userId);
    Task<IEnumerable<LikeDto>> GetUserLikes(string predicate, int userId);
}
