using Storyteller.Models;

namespace Storyteller.Services
{
    public interface IStoryService
    {
        Task<IEnumerable<Story>> GetAllAsync();
        Task<Story> GetByIdAsync(long id);
        Task<Story> AddAsync(Story story);
        Task<Story> UpdateAsync(Story story);
        Task<bool> DeleteAsync(long id);
        Task<IEnumerable<Story>> GetByNameAsync(string name);
        Task<IEnumerable<Story>> GetByNameContainingAsync(string name);
        Task<IEnumerable<Story>> GetByAuthorContainingAsync(string name);
        Task<IEnumerable<Story>> GetByTagsContainingAsync(string name);
    }
}
