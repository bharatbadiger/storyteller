using Storyteller.Models;

namespace Storyteller.Services
{
public interface IUserFollowAuthorService
{
    Task<UserFollowAuthor> FollowAsync(long userId, long authorId);
    Task UnfollowAsync(long userId, long authorId);
    Task<IEnumerable<Author>> GetAuthorsByUserIdAsync(long userId);
    Task<IEnumerable<User>> GetUsersByAuthorIdAsync(long authorId);
}

}
