using Microsoft.EntityFrameworkCore;
using Storyteller.Data;
using Storyteller.Models;

namespace Storyteller.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                                 .Include(u => u.Subscription)
                                 .ToListAsync();
        }

        public async Task<User> GetByIdAsync(long id)
        {
            return await _context.Users
                                 .Include(u => u.Subscription)
                                 .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByFirebaseAuthIdAsync(string id)
        {
            return await _context.Users
                                 .Include(u => u.Subscription)
                                 .FirstOrDefaultAsync(u => u.FirebaseAuthId == id);
        }
        public async Task<User> GetByMobileNumberAsync(string mobile)
        {
            return await _context.Users
                                 .Include(u => u.Subscription)
                                 .FirstOrDefaultAsync(u => u.Mobile == mobile);
        }

        public async Task<User> AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<User>> GetByNameAsync(string name)
        {
            return await _context.Users
                                 .Where(u => u.Name.Contains(name))
                                 .Include(u => u.Subscription)
                                 .ToListAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users
                                 .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
