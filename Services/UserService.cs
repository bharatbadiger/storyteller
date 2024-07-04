using Storyteller.Models;
using Storyteller.Repositories;

namespace Storyteller.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(long id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> GetByFirebaseAuthIdAsync(string id)
        {
            return await _userRepository.GetByFirebaseAuthIdAsync(id);
        }

        public async Task<User> GetByMobileNumberAsync(string mobile)
        {
            return await _userRepository.GetByMobileNumberAsync(mobile);
        }

        public async Task<User> AddAsync(User user)
        {
            var existingUser = await _userRepository.GetByEmailAsync(user.Email);
            if (existingUser != null)
            {
                throw new Exception("User with this email already exists.");
            }

            return await _userRepository.AddAsync(user);
        }

        public async Task<User> UpdateAsync(User user)
        {
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<User>> GetByNameAsync(string name)
        {
            return await _userRepository.GetByNameAsync(name);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }
    }
}
