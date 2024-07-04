using Storyteller.Models;
using Storyteller.Repositories;

namespace Storyteller.Services
{
    public class UserFollowAuthorService : IUserFollowAuthorService
    {
        private readonly IUserFollowAuthorRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IAuthorRepository _authorRepository;

        public UserFollowAuthorService(
            IUserFollowAuthorRepository repository,
            IUserRepository userRepository,
            IAuthorRepository authorRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _authorRepository = authorRepository;
        }

        public async Task<UserFollowAuthor> FollowAsync(long userId, long authorId)
        {
            var user = await _userRepository.GetByIdAsync(userId) ?? throw new NotFoundException("User not found");
            var author = await _authorRepository.GetByIdAsync(authorId) ?? throw new NotFoundException("Author not found");
            var existingFollow = await _repository.FindByUserIdAndAuthorIdAsync(userId, authorId);
            if (existingFollow != null)
                throw new ConflictException("User is already following the author");

            var userFollowAuthor = new UserFollowAuthor { User = user, Author = author };
            await _repository.AddAsync(userFollowAuthor);
            return userFollowAuthor;
        }

        public async Task UnfollowAsync(long userId, long authorId)
        {
            _ = await _userRepository.GetByIdAsync(userId) ?? throw new NotFoundException("User not found");
            _ = await _authorRepository.GetByIdAsync(authorId) ?? throw new NotFoundException("Author not found");
            var follow = await _repository.FindByUserIdAndAuthorIdAsync(userId, authorId);
            if (follow != null)
            {
                await _repository.DeleteAsync(follow);
            } else {
                throw new UserNotFollowingException("User is Not Following the Author");
            }
        }

        public async Task<IEnumerable<Author>> GetAuthorsByUserIdAsync(long userId)
        {
            return await _repository.GetAuthorsByUserIdAsync(userId);
        }

        public async Task<IEnumerable<User>> GetUsersByAuthorIdAsync(long authorId)
        {
            return await _repository.GetUsersByAuthorIdAsync(authorId);
        }
    }

}
