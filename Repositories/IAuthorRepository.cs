using Storyteller.Models;

namespace Storyteller.Repositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author> GetByIdAsync(long id);
        Task AddAsync(Author author);
        Task UpdateAsync(Author author);
        Task DeleteAsync(long id);
        Task<Author> GetByNameAsync(string name);
        Task<IEnumerable<Author>> GetByNameContainingAsync(string name);
    }
}
