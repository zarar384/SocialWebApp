using AutoMapper;
using SocialWebAPI.Db;
using SocialWebAPI.Interfaces;

namespace SocialWebAPI;

public class UnitOfWork : IUnitOfWork
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;
    public UnitOfWork(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IUserRepository UserRepository => new UserRepository(_context, _mapper);

    public IMessageRepository MessageRepository => new MessageRepository(_context, _mapper);

    public ILikesRepository LikesRepository => new LikesRepository(_context);

    public async Task<bool> Complete()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public bool HasChanges()
    {
        return _context.ChangeTracker.HasChanges();
    }
}
