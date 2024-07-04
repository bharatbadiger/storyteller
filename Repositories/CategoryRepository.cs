using Microsoft.EntityFrameworkCore;
using Storyteller.Data;
using Storyteller.Models;

namespace Storyteller.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(long id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category> AddAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return false;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Category>> GetByNameAsync(string name)
        {
            return await _context.Categories
                                 .Where(c => c.Name.Contains(name))
                                 .ToListAsync();
        }
        public async Task<IEnumerable<Category>> GetByNameContainingAsync(string name)
        {
            return await _context.Categories
                                 .Where(a => a.Name.Contains(name))
                                 .ToListAsync();
        }
    }
}
