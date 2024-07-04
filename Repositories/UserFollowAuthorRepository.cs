using Microsoft.EntityFrameworkCore;
using Storyteller.Data;
using Storyteller.Models;

namespace Storyteller.Repositories
{
    public class UserFollowAuthorRepository : IUserFollowAuthorRepository
{
    private readonly ApplicationDbContext _context;

    public UserFollowAuthorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserFollowAuthor> FindByUserIdAndAuthorIdAsync(long userId, long authorId)
    {
        return await _context.UserFollowAuthors
            .FirstOrDefaultAsync(ufa => ufa.User.Id == userId && ufa.Author.Id == authorId);
    }

    public async Task AddAsync(UserFollowAuthor userFollowAuthor)
    {
        await _context.UserFollowAuthors.AddAsync(userFollowAuthor);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(UserFollowAuthor userFollowAuthor)
    {
        _context.UserFollowAuthors.Remove(userFollowAuthor);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Author>> GetAuthorsByUserIdAsync(long userId)
    {
        return await _context.UserFollowAuthors
            .Where(ufa => ufa.User.Id == userId)
            .Select(ufa => ufa.Author)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetUsersByAuthorIdAsync(long authorId)
    {
        return await _context.UserFollowAuthors
            .Where(ufa => ufa.Author.Id == authorId)
            .Select(ufa => ufa.User)
            .ToListAsync();
    }
}

}