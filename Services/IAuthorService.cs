using Storyteller.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storyteller.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author> GetByIdAsync(long id);
        Task<Author> AddAsync(Author author);
        Task<Author> UpdateAsync(Author author);
        Task<bool> DeleteAsync(long id);
        Task<IEnumerable<Author>> GetByNameAsync(string name);
    }
}
