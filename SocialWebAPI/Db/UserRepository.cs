using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SocialWebAPI.Entities;
using SocialWebAPI.Helpers;
using SocialWebAPI.Interfaces;

namespace SocialWebAPI.Db
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AppUser> GetByIdAsync(int id)
        {
            return await _context.AppUsers.FindAsync(id);
        }

        public async Task<AppUser> GetByUserNameAsync(string userName)
        {
            return await _context.AppUsers
                .Include(i => i.Photos)
                .SingleOrDefaultAsync(x => x.UserName == userName);
        }

        public async Task<MemberDto> GetMemberByNameAsync(string memberName)
        {
            return await _context.AppUsers
                .Where(x => x.UserName == memberName)
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<PageList<MemberDto>> GetMembersAsync(UserParams userParams)
        {
            var query = _context.AppUsers
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .AsNoTracking();

            return await PageList<MemberDto>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.AppUsers
                .Include(i => i.Photos)
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0; //something in db = true
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}
