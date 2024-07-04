using Storyteller.Models;

namespace Storyteller.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(long id);
        Task<Category> AddAsync(Category category);
        Task<Category> UpdateAsync(Category category);
        Task<bool> DeleteAsync(long id);
        Task<IEnumerable<Category>> GetByNameAsync(string name);
        Task<IEnumerable<Category>> GetByNameContainingAsync(string name);
    }
}
