using Storyteller.Models;

namespace Storyteller.Repositories
{
public interface IUserFollowAuthorRepository
{
    Task<UserFollowAuthor> FindByUserIdAndAuthorIdAsync(long userId, long authorId);
    Task AddAsync(UserFollowAuthor userFollowAuthor);
    Task DeleteAsync(UserFollowAuthor userFollowAuthor);
    Task<IEnumerable<Author>> GetAuthorsByUserIdAsync(long userId);
    Task<IEnumerable<User>> GetUsersByAuthorIdAsync(long authorId);
}

}
