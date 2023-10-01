using Microsoft.EntityFrameworkCore;
using SocialWebAPI.Db;
using SocialWebAPI.Entities;
using SocialWebAPI.Extensions;
using SocialWebAPI.Helpers;

namespace SocialWebAPI;

public class LikesRepository : ILikesRepository
{
    private readonly AppDbContext _context;
    public LikesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserLike> GetUserLike(int sourceUserId, int targetUserId)
    {
        return await _context.Likes.FindAsync(sourceUserId, targetUserId);
    }

    public async Task<PageList<LikeDto>> GetUserLikes(LikesParams likesParams)
    {
        var users = _context.AppUsers.OrderBy(u => u.UserName).AsQueryable();
        var likes = _context.Likes.AsQueryable();

        if (likesParams.Predicate == "liked")
        {
            likes = likes.Where(like => like.SourceUserId == likesParams.UserId);
            users = likes.Select(like => like.TargetUser);
        }

        if (likesParams.Predicate == "likedBy")
        {
            likes = likes.Where(like => like.TargetUserId == likesParams.UserId);
            users = likes.Select(like => like.SourceUser);
        }

        var likedUsers = users.Select(user => new LikeDto
        {
            UserName = user.UserName,
            KnownAs = user.KnownAs,
            Age = user.DateOfBirth.CalculateAge(),
            PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain).Url,
            Id = user.AppUserId,
        });

        return await PageList<LikeDto>.CreateAsync(likedUsers, likesParams.PageNumber, likesParams.PageSize);
    }

    public async Task<AppUser> GetUserWithLikes(int userId)
    {
        return await _context.AppUsers
            .Include(x => x.LikedUsers)
            .FirstOrDefaultAsync(x => x.AppUserId == userId);
    }
}
