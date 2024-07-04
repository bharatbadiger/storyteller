using Storyteller.Models;
using Storyteller.Repositories;

namespace Storyteller.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetByIdAsync(long id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<Category> AddAsync(Category category)
        {
            var existingCategory = await _categoryRepository.GetByNameAsync(category.Name);
            if (existingCategory.Any())
            {
                throw new Exception("Category already exists");
            }
            return await _categoryRepository.AddAsync(category);
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            return await _categoryRepository.UpdateAsync(category);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _categoryRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Category>> GetByNameAsync(string name)
        {
            return await _categoryRepository.GetByNameAsync(name);
        }
    }
}
