using Storyteller.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storyteller.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(long id);
        Task<User> GetByFirebaseAuthIdAsync(string id);
        Task<User> GetByMobileNumberAsync(string mobile);
        Task<User> AddAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<bool> DeleteAsync(long id);
        Task<IEnumerable<User>> GetByNameAsync(string name);
        Task<User> GetByEmailAsync(string email);
    }
}
