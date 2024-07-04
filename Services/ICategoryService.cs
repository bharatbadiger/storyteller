using Storyteller.Models;

namespace Storyteller.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(long id);
        Task<Category> AddAsync(Category category);
        Task<Category> UpdateAsync(Category category);
        Task<bool> DeleteAsync(long id);
        Task<IEnumerable<Category>> GetByNameAsync(string name);
    }
}
